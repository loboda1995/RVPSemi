import java.io.*;
import java.net.Socket;
import java.sql.Time;

public class TCPClient {
    public static void main(String argv[]) throws Exception {
        Socket clientSocket = new Socket("localhost", 8052);
        PrintWriter pw = new PrintWriter(clientSocket.getOutputStream());
        InputStream is = clientSocket.getInputStream();
        byte[] buffer = new byte[1024];
        int read;
        while((read = is.read(buffer)) != -1) {
            String output = new String(buffer, 0, read);
            System.out.println(output);
            System.out.flush();
            pw.println("COMMAND");
            pw.flush();
        }
        clientSocket.close();
    }
}
