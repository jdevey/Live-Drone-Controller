﻿﻿using System;
 using System.Threading;
 using NUnit.Framework;
using DroneController;
 

namespace UnitTests
{
		[TestFixture]
		public class DroneUDPClientTests
		{
			[Test]
			public void Main()
			{
//				DroneSimulator droneSimulator = new DroneSimulator(
//					ref TestConstants.simulatorIp,
//					ref TestConstants.simulatorPort);
				
				DroneUDPClient udpClient1 = new DroneUDPClient(
					ref TestConstants.droneIp,
					ref TestConstants.dronePort);
				udpClient1.startConnection();
				
				Assert.IsFalse(udpClient1.getErrorState(), "Drone in error state.");

//					droneSimulator.closeClient();
			}
		}
}