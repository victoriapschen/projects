import javax.net.ssl.*;
import java.security.KeyStore;
import java.security.cert.X509Certificate;
import javax.naming.ldap.*;
import javax.net.*;
import java.io.*;

public class MyTLSFileServer {
	public static void main(String args[])
	{
	/*
	* use the getSSF method to get a
	* SSLServerSocketFactory and create our
	* SSLServerSocket, bound to specified port
	*/
	while(true){
	try {
	//checks that 1 arguement was given
	if (args.length==1){
	//gets the given port from the arguement
	int port=Integer.parseInt(args[0]);
	ServerSocketFactory ssf = getSSF();
	SSLServerSocket ss = (SSLServerSocket) ssf.createServerSocket(port);
	String EnabledProtocols[] = {"TLSv1.2", "TLSv1.1"};
	ss.setEnabledProtocols(EnabledProtocols);
	SSLSocket s = (SSLSocket)ss.accept();
	BufferedReader reader = new BufferedReader(new InputStreamReader(s.getInputStream()));
	String filename = reader.readLine();
	//prints out the file name received
	System.out.println(filename);    
	//gets the file
	FileInputStream file = new FileInputStream(filename);
	ss.close();
	s.close();
	}
	//if wrong number of arguements entered, show message and return
	else
	{System.out.println("Please enter only a <port>"); 
	return;}
	}//end try
	catch (Exception e)
	{
	System.err.println("Exception: " + e);
	}//end catch
	}//endwhile
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
} //end MyTLSFileServer class
