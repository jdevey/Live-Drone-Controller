﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace UnitTests
{
	public class Server
	{
		private readonly HashSet<string> validCommands = new HashSet<string>()
		{
			"command",
			"takeoff",
			"land",
			"up",
			"down",
			"left",
			"right",
			"forward",
			"back",
			"cw",
			"ccw",
			"flip",
			"go",
			"stop"
		};

		private IPEndPoint ipEndpoint;
		private IPEndPoint dataSender;
		private UdpClient udpClient;
		private readonly Thread thread;
		// public bool go = true;

		public Server(int port = 8891)
		{
			ipEndpoint = new IPEndPoint(IPAddress.Any, 8891); // IPAdress.Any
			dataSender = new IPEndPoint(IPAddress.Any, 8889);
			udpClient = new UdpClient(ipEndpoint)
				{Client = {SendTimeout = 3000, ReceiveTimeout = 3000}};

			thread = new Thread(udpLoop);
		}

		public void start()
		{
			// udpLoop();
			thread.Start();
			Thread.Sleep(100); // Give the thread time to start before sending a message
		}
		
		// https://stackoverflow.com/questions/4844581/how-do-i-make-a-udp-server-in-c
		void udpLoop() {
			while(true)
			{
				byte[] dataBuffer = udpClient.Receive(ref dataSender);
				string messageReceived = Encoding.UTF8.GetString(dataBuffer);
				string commandType = messageReceived.Split(null)[0];
				bool validCommand = validCommands.Contains(commandType);
				byte[] messageToSend = Encoding.UTF8.GetBytes(validCommand ? "ok" : "error");

				Console.WriteLine(Encoding.ASCII.GetString(dataBuffer, 0, dataBuffer.Length));
				
				udpClient.Send(messageToSend, messageToSend.Length, dataSender);
			}
		}
		
		public void closeClient()
		{
			thread.Join();
//			go = false;
		}
	}
}