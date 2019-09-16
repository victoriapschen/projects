import javax.net.ssl.*;
import java.security.KeyStore;
import java.security.cert.X509Certificate;
import javax.naming.ldap.*;
import javax.net.*;
import java.io.*;

public class MyTLSFileClient {
	public static void main(String args[])
	{
		try{
			//checks that there's 3 arguements given
			if (args.length==3){
			//gets the given values from the arguement
			String host = args[0];
			int port=Integer.parseInt(args[1]);
			String filename = args[2];
			SSLSocketFactory factory = (SSLSocketFactory)SSLSocketFactory.getDefault();
			SSLSocket socket = (SSLSocket)factory.createSocket(host, port);

			/*
			 * set HTTPS-style checking of HostName
			 * before the handshake commences
			 */
			SSLParameters params = new SSLParameters();
			params.setEndpointIdentificationAlgorithm("HTTPS");
			socket.setSSLParameters(params);
			socket.startHandshake();

			/* get the X509Certificate for this session */
			SSLSession sesh = socket.getSession();
			X509Certificate cert = (X509Certificate)
			sesh.getPeerCertificates()[0];
			/* extract the CommonName, and then compare */
			System.out.println(getCommonName(cert));

			/*
			 * at this point, can getInputStream and
			 * getOutputStream as you would a regular Socket
			 */
			//gets the file
			FileInputStream file = new FileInputStream(filename);
			PrintWriter writer = new PrintWriter(socket.getOutputStream(), true);
			writer.println(filename);
			socket.close();
			}
			//if not 3 arguements then message
			else
			{System.out.println("Please enter a <host> <port> <filename>"); }
		}//end try

		catch (Exception e)
		{
			e.printStackTrace();
			System.err.println("Exception: " + e);
		}//end catch
	}//end main

	public static ServerSocketFactory getSSF()
	{
		try {	
			/*
			 * Get an SSL Context that speaks some version
			 * of TLS, a KeyManager that can hold certs in
			 * X.509 format, and a JavaKeyStore (JKS)
			 * instance
			 */
			SSLContext ctx = SSLContext.getInstance("TLS");
			KeyManagerFactory kmf = KeyManagerFactory.getInstance("SunX509");
			KeyStore ks = KeyStore.getInstance("JKS");

			/*
			 * store the passphrase to unlock the JKS file.
			 * completely insecure, there are better ways.
			 * DON’T DO THIS!
			 */
			char[] passphrase = "773999".toCharArray();
			/*
			 * load the keystore file. The passhrase is
			 * an optional parameter to allow for integrity
			 * checking of the keystore. Could be null
			 */
			ks.load(new FileInputStream("server.jks"),
					passphrase); 

			/*
			 * init the KeyManagerFactory with a source
			 * of key material. The passphrase is necessary
			 * to unlock the private key contained.
			 */
			kmf.init(ks, passphrase);
			/*
			 * initialise the SSL context with the keys.
			 */
			ctx.init(kmf.getKeyManagers(), null, null);

			/*
			 * get the factory we will use to create
			 * our SSLServerSocket
			 */
			SSLServerSocketFactory ssf = ctx.getServerSocketFactory();
			return ssf;
		}//end try
		catch (Exception e)
		{
			System.err.println("Exception: " + e);
		}//end catch
		return null;
	}//end ServerSocketFactory


	public static String getCommonName(X509Certificate cert)
	{
		try {
			String name = cert.getSubjectX500Principal().getName();
			LdapName ln = new LdapName(name);
			String cn = null;
			for(Rdn rdn : ln.getRdns())
				if("CN".equalsIgnoreCase(rdn.getType()))
					cn = rdn.getValue().toString();
			return cn;
		}//endtry
		catch (Exception e)
		{
			System.err.println("Exception: " + e);
			return null;
		}//end catch
	} //end getCommonName
}//end MyTLSFileClient class
