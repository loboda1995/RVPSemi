import java.io.*;
import java.net.Socket;

public class TCPClient {
    private static float Kp1, Ti1, Td1;
    private static float Kp2, Ti2, Td2;

    private static void readParameters() throws Exception{
        float Ki1, Kd1;
        float Ki2, Kd2;

        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        // vnos parametrov za PID, ki drži nihalo v pokončnem položaju
        System.out.println("Hold parameters.");
        System.out.print("Kp: ");
        String input = br.readLine();
        if(input.length() > 0){
            Kp1 = Float.parseFloat(input);
        }else{
            Kp1 = 0;
        }
        System.out.print("Ki: ");
        input = br.readLine();
        if(input.length() > 0){
            Ki1 = Float.parseFloat(input);
        }else{
            Ki1 = 0;
        }
        System.out.print("Kd: ");
        input = br.readLine();
        if(input.length() > 0){
            Kd1 = Float.parseFloat(input);
        }else{
            Kd1 = 0;
        }
        System.out.print("Ti: ");
        input = br.readLine();
        if(input.length() > 0){
            Ti1 = Float.parseFloat(input);
        }else{
            Ti1 = Kp1 / Ki1;
        }
        System.out.print("Td: ");
        input = br.readLine();
        if(input.length() > 0){
            Td1 = Float.parseFloat(input);
        }else {
            Td1 = Kd1 / Kp1;
        }
        // vnos parametrov za PID, ki dvigne nihalo
        System.out.println("Lift parameters.");
        System.out.print("Kp: ");
        input = br.readLine();
        if(input.length() > 0){
            Kp2 = Float.parseFloat(input);
        }else{
            Kp2 = 0;
        }
        System.out.print("Ki: ");
        input = br.readLine();
        if(input.length() > 0){
            Ki2 = Float.parseFloat(input);
        }else{
            Ki2 = 0;
        }
        System.out.print("Kd: ");
        input = br.readLine();
        if(input.length() > 0){
            Kd2 = Float.parseFloat(input);
        }else{
            Kd2 = 0;
        }
        System.out.print("Ti: ");
        input = br.readLine();
        if(input.length() > 0){
            Ti2 = Float.parseFloat(input);
        }else{
            Ti2 = Kp2 / Ki2;
        }
        System.out.print("Td: ");
        input = br.readLine();
        if(input.length() > 0){
            Td2 = Float.parseFloat(input);
        }else {
            Td2 = Kd2 / Kp2;
        }
    }

    public static void main(String argv[]) throws Exception {

        readParameters();

        Socket clientSocket = new Socket("localhost", 8052);
        PrintWriter pw = new PrintWriter(clientSocket.getOutputStream());
        InputStream is = clientSocket.getInputStream();
        byte[] buffer = new byte[64];
        int read;

        float e_old = 0, integral = 0;

        float Kp, Ti, Td, e, de;
        double P, I, D, u;

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
                    Kp = Kp1;
                    Ti = Ti1;
                    Td = Td1;
                }else{
                    Kp = Kp2;
                    Ti = Ti2;
                    Td = Td2;
                }

                integral += (e * dt);

                P = Kp * e;
                I = (Kp/Ti) * integral;
                D = Kp * Td * (de/dt);


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