using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;


using AppKit;

namespace Hello_Mac_CS
{
	static class MainClass
	{
		static void Main (string[] args)
		{
			NSPasteboard
			Console.WriteLine("Hello Mac from CS!");

			IPAddress ipAddress =  IPAddress.Parse ("127.0.0.1");

			TcpListener serverSocket = new TcpListener (ipAddress , 8888);
			int requestCount = 0;
			TcpClient clientSocket = default(TcpClient);
			serverSocket.Start();
			Console.WriteLine(" >> Server Started");
			clientSocket = serverSocket.AcceptTcpClient();
			Console.WriteLine(" >> Accept connection from client");
			requestCount = 0;

			while ((true))
			{
				try
				{
					Console.WriteLine("Reading from the scoket......");
					requestCount = requestCount + 1;
					Console.WriteLine("Request number : " + requestCount);
					NetworkStream networkStream = clientSocket.GetStream();
					byte[] bytesFrom = new byte[10025];
					networkStream.Read(bytesFrom, 0, ((int)clientSocket.ReceiveBufferSize - 1));
					string dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
					dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
					Console.WriteLine(" >> Data from client - " + dataFromClient);
					string serverResponse = "Saying hello to cocoa.";// + dataFromClient;
					Byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);
					networkStream.Write(sendBytes, 0, sendBytes.Length);
					networkStream.Flush();
					Console.WriteLine(" >> " + serverResponse);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString() + serverSocket);

					//serverSocket = new TcpListener (ipAddress , 8888);
					//clientSocket = default(TcpClient);
					//serverSocket.Start();
					//Console.WriteLine(" >> Server Started Again");
					//clientSocket = serverSocket.AcceptTcpClient();
					//Console.WriteLine(" >> Accept connection from client");
					//requestCount = 0;
				}
			}

			/*clientSocket.Close();
			serverSocket.Stop();
			Console.WriteLine(" >> exit");
			Console.ReadLine();*/



			//NSApplication.Init ();
			//NSApplication.Main (args);
		}
	}
}
