import java.net.*;
import java.io.*;
import java.util.*;

class ChatClient {
  public static void main(String args[])
  {
  try{
    InetAddress group = InetAddress.getByName("239.0.202.1");
    MulticastSocket ms = new MulticastSocket(40202);
    //makes a new thread to listen in on chat
    ChatClientSession thread = new ChatClientSession();
    thread.start();
    //reads what keyboard typed
    BufferedReader reader = new BufferedReader(new InputStreamReader(System.in)); 
    //send messages to chat
    while (true) {
      //reads
      String line = reader.readLine();
      //sends the message
      DatagramPacket msg = new DatagramPacket(line.getBytes(), line.length(),group, 40202);
      ms.send(msg);
    }//endwhile
  }

catch(Exception e) {System.err.println("Exception: " + e);}
  }//end main

}//end chatclient class

class ChatClientSession extends Thread {
  public void run() {
  try {
  InetAddress group = InetAddress.getByName("239.0.202.1");
  MulticastSocket ms = new MulticastSocket(40202);
    ms.joinGroup(group);
    //listen in on chat
    byte[] buf = new byte[1000];
    while (true){
      DatagramPacket recv = new DatagramPacket(buf, buf.length);
      ms.receive(recv);
      String address = recv.getAddress().getHostAddress();
      String s = new String(buf, 0, recv.getLength());
      System.out.println(address+": "+s);
    }//end while
  }//endtry

    catch(Exception e) {System.err.println("Exception: " + e);}
  }//end run

}//end ChatClientSession thread
