import java.io.*;
import java.net.Socket;
import java.sql.Time;
import java.util.Random;

public class TCPClient {

    public static void main(String argv[]) throws Exception {
        Socket clientSocket = new Socket("localhost", 8052);
        PrintWriter pw = new PrintWriter(clientSocket.getOutputStream());
        InputStream is = clientSocket.getInputStream();
        byte[] buffer = new byte[1024];
        int read;

        while((read = is.read(buffer)) != -1) {
            String output = new String(buffer, 0, read);
            float theta = Float.parseFloat(output);
            float e = theta < 180 ? theta : theta - 360;
            System.out.println(e);

            pw.printf("%f", e * 0.3);
            pw.flush();
        }
        clientSocket.close();
    }
}
