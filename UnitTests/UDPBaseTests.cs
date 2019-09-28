﻿using System;
using System.Threading;
using NUnit.Framework;
using DroneController;

namespace UnitTests
{
	[TestFixture]
	public class UDPBaseTests
	{
		[Test]
		public void Main()
		{
//			DroneSimulator droneSimulator = new DroneSimulator(
//				ref TestConstants.simulatorIp,
//				ref TestConstants.simulatorPort);
				
			DroneUDPClient udpClient1 = new DroneUDPClient(
				ref TestConstants.droneIp,
				ref TestConstants.dronePort);
			udpClient1.startConnection();
				
			Assert.IsFalse(udpClient1.getErrorState(), "Drone in error state.");
				
			udpClient1.sendMessage("takeoff");
			string response = udpClient1.getResponse();
			Console.WriteLine(response);
			Assert.IsFalse(udpClient1.getErrorState(), "Drone in error state.");
			Assert.AreEqual(response, "ok");

			udpClient1.sendMessage("land");
			response = udpClient1.getResponse();
			Assert.IsFalse(udpClient1.getErrorState(), "Drone in error state.");
			Assert.AreEqual(response, "ok");

			udpClient1.sendMessage("garbage command");
			response = udpClient1.getResponse();
			Assert.AreNotEqual(response, "ok");

			string droneIp2 = "g.hh";
			DroneUDPClient udpClient2 = new DroneUDPClient(ref droneIp2, ref TestConstants.dronePort);
			Assert.IsTrue(udpClient2.getErrorState(), "Drone not in error state.");

			udpClient2.setErrorState(false);
			Assert.IsFalse(udpClient2.getErrorState());
			udpClient2.setErrorState(true);
			Assert.IsTrue(udpClient2.getErrorState());
			
//			droneSimulator.closeClient();
		}
	}
}