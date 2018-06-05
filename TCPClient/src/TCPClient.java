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
        float integral = 0;

        float Kp, Ki, Kd;
        double u, D;
        float P, I, e, de;
        String output;

        // konstanten čas
        double dt = 0.02;
        while((read = is.read(buffer)) != -1) {
            output = new String(buffer, 0, read);
            if(output.contains("RESET;")) {
                integral = 0;
                e_old = 0;
            }else {
                e = Float.parseFloat(output.split(";")[0]); // angle
                de = e - e_old;

                if(Math.abs(e) < 1.2){
                    Kp = 12f;
                    Ki = 3.0f;
                    Kd = 0.55f;
                }else{
                    Kp = 0.4f;
                    Ki = 1.8f;
                    Kd = 0.0f;
                }

                integral += (e * dt);
                if (integral > 150)
                    integral = 150;
                if (integral < -150)
                    integral = -150;

                P = Kp * e;
                I = Ki * integral;
                D = Kd * (de / dt);
                u = P + I + D;

                // ojačanje
                u *= 10;

                pw.printf("%f", u);
                pw.flush();

                e_old = e;
            }
        }
        clientSocket.close();
    }
}