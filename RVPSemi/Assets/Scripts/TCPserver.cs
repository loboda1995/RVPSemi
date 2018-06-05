using System;
using System.Collections; 
using System.Collections.Generic; 
using System.Net; 
using System.Net.Sockets; 
using System.Text; 
using System.Threading; 
using UnityEngine;  

public class TCPserver : MonoBehaviour {  	
	public InvertedPendulum pendulum;
	private TcpListener tcpListener; 
	private Thread tcpListenerThread;  	
	private TcpClient connectedTcpClient; 	
	private NetworkStream stream;
	public GameObject clientOn;
	public GameObject clientOff;

	// Use this for initialization
	void Start () { 		
		// Start TcpServer background thread 		
		tcpListenerThread = new Thread (new ThreadStart(ListenForIncommingRequests)); 		
		tcpListenerThread.IsBackground = true; 		
		tcpListenerThread.Start(); 	

		InvokeRepeating("SendMessage", 1.0f, 0.02f);
	}  	

	// Update is called once per frame
	void Update () { 		
		clientOn.SetActive (connectedTcpClient != null);
		clientOff.SetActive (connectedTcpClient == null);
	}  	

	private bool sendReset = false;
	public void SendReset() {
		sendReset = true;
	}

	/// <summary> 	
	/// Runs in background TcpServerThread; Handles incomming TcpClient requests 	
	/// </summary> 	
	private void ListenForIncommingRequests () { 		
		try { 			
			// Create listener on localhost port 8052. 			
			tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8052); 			
			tcpListener.Start();              
			Debug.Log("Server is listening");              
			Byte[] bytes = new Byte[64];  			
			while (true) { 	
				using (connectedTcpClient = tcpListener.AcceptTcpClient()) { 					
					// Get a stream object for reading 		
					using (stream = connectedTcpClient.GetStream()) { 
						int length; 
						// Read incomming stream into byte arrary. 	
						try{
							while ((length = stream.Read(bytes, 0, bytes.Length)) != 0) { 							
								var incommingData = new byte[length]; 							
								Array.Copy(bytes, 0, incommingData, 0, length);  							
								// Convert byte array to string message. 							
								string clientMessage = Encoding.ASCII.GetString(incommingData); 	
								float command = float.Parse(clientMessage);
								pendulum.SetForce(command);
							} 
						}
						catch (Exception e){
							stream.Close();
						}
					} 
				} 
			} 	
		} 		
		catch (SocketException socketException) { 			
			Debug.Log("SocketException " + socketException.ToString()); 		
		}
	}  	
	/// <summary> 	
	/// Send message to client using socket connection. 	
	/// </summary> 	

	private byte[] serverMessageAsByteArray;
	private string serverMessage;
	private float e;
	private void SendMessage() { 		
		if (connectedTcpClient == null) {             
			return;         
		}  		

		try { 			
			// Get a stream object for writing. 			
			NetworkStream stream = connectedTcpClient.GetStream(); 	

			if (stream.CanWrite) {
				if(sendReset){
					serverMessage = "RESET;";
					sendReset = false;
				}else{
					e = pendulum.pendulum.transform.localEulerAngles.y;
					if (e > 180f)
						e = e - 360f;
					e = e * Mathf.Deg2Rad;
					serverMessage = String.Format("{0};", e); 			
									
				}
				// Convert string message to byte array.                 
				serverMessageAsByteArray = Encoding.ASCII.GetBytes(serverMessage); 
				// Write byte array to socketConnection stream.               
				stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);               
				stream.Flush();
			}       
		} 		
		catch (SocketException socketException) {             
			Debug.Log("Socket exception: " + socketException);         
		} 
		catch (ObjectDisposedException e){
			connectedTcpClient.Close ();
			connectedTcpClient = null;
			pendulum.SetForce(0);
		}	
	} 
}
