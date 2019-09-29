//﻿using System;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading;
//
//namespace UnitTests
//{
//	// Third-party class I tried to use
//	// https://gist.github.com/darkguy2008/413a6fea3a5b4e67e5e0d96f750088a9
//	public class UDPSocket
//	{
//		private Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
//		private const int bufSize = 8 * 1024;
//		private State state = new State();
//		private EndPoint epFrom = new IPEndPoint(IPAddress.Any, 0);
//		private AsyncCallback recv = null;
//		
//		// private readonly Thread thread;
//
//		public UDPSocket()
//		{
//			// thread = new Thread(udpLoop);
//		}
//		
//		public void start()
//		{
//			// udpLoop();
//			// udpLoop();
//			Thread.Sleep(100); // Give the thread time to start before sending a message
//		}
//
////		private void udpLoop()
////		{
////			while(true)
////			{
////				byte[] dataBuffer = Receive(ref dataSender);
////				string messageReceived = Encoding.UTF8.GetString(dataBuffer);
////				string commandType = messageReceived.Split(null)[0];
////				bool validCommand = validCommands.Contains(commandType);
////				byte[] messageToSend = Encoding.UTF8.GetBytes(validCommand ? "ok" : "error");
////
////				Console.WriteLine(Encoding.ASCII.GetString(dataBuffer, 0, dataBuffer.Length));
////				
////				udpClient.Send(messageToSend, messageToSend.Length, dataSender);
////			}
////		}
//
//		public class State
//		{
//			public byte[] buffer = new byte[bufSize];
//		}
//
//		public void Server(string address, int port)
//		{
//			_socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
//			_socket.Bind(new IPEndPoint(IPAddress.Parse(address), port));
//			Receive();            
//		}
//
//		public void Client(string address, int port)
//		{
//			_socket.Connect(IPAddress.Parse(address), port);
//			// Receive();
//		}
//
//		public void Send(string text)
//		{
//			byte[] data = Encoding.ASCII.GetBytes(text);
//			_socket.BeginSend(data, 0, data.Length, SocketFlags.None, (ar) =>
//			{
//				State so = (State)ar.AsyncState;
//				int bytes = _socket.EndSend(ar);
//				Console.WriteLine("SEND: {0}, {1}", bytes, text);
//			}, state);
//		}
//
//		private void Receive()
//		{            
//			_socket.BeginReceiveFrom(state.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv = (ar) =>
//			{
//				State so = (State)ar.AsyncState;
//				int bytes = _socket.EndReceiveFrom(ar, ref epFrom);
//				_socket.BeginReceiveFrom(so.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv, so);
//				Send(Encoding.ASCII.GetString(so.buffer, 0, bytes));
//				// Console.WriteLine("RECV: {0}: {1}, {2}", epFrom.ToString(), bytes, Encoding.ASCII.GetString(so.buffer, 0, bytes));
//			}, state);
//		}
//	}
//}