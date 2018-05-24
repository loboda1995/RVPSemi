import java.io.*;
import java.net.Socket;
import java.time.Instant;

public class TCPClient {

    public static void main(String argv[]) throws Exception {
        Socket clientSocket = new Socket("localhost", 8052);
        PrintWriter pw = new PrintWriter(clientSocket.getOutputStream());
        InputStream is = clientSocket.getInputStream();
        byte[] buffer = new byte[64];
        int read;

        float e_old = 0;
        double t_old = System.currentTimeMillis();
        float integral = 0;

        float Kp = 35f;
        float Ki = 0.7f;
        float Kd = 15.0f;
        double u, D, t, dt;
        float P, I, e, de;
        String output;

        while((read = is.read(buffer)) != -1) {
            output = new String(buffer, 0, read);
            e = Float.parseFloat(output.split(";")[0]); // angle
            t = System.currentTimeMillis();
            dt = (t - t_old);
            de = e - e_old;

            integral += (e * dt);
            if(integral > 50)
                integral = 50;
            if(integral < -50)
                integral = -50;

            P = Kp * e;
            I = Ki * integral;
            D = Kd * (de / dt);
            u = P + I + D;

            if(!Double.isNaN(u)) {
                pw.printf("%f", u);
                pw.flush();
            }

            t_old = t;
            e_old = e;
        }
        clientSocket.close();
    }
}