using System;
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;
using System.Threading;
using DroneController;
using NUnit.Framework;
using Shared;

namespace UnitTests
{
	[TestFixture, NonParallelizable, SingleThreaded]
	public class ControllerCommTests
	{
		[Test, OneTimeSetUp, OneTimeTearDown, NonParallelizable]
		public void Main()
		{
			//var available = IPGlobalProperties.GetIPGlobalProperties().GetActiveUdpListeners();
			Console.WriteLine("I'm running !!!");

			Simulator.Simulator simulator = TestingUtils.generateSim(TestingUtils.ports[0]);
			simulator.start();
			//Controller controller = new Controller(localPort: DefaultConstants.DEFAULT_LOCAL_PORT);
			Controller controller = TestingUtils.generateContr(TestingUtils.ports[0]);
			controller.getUDPClient().startConnection();
			//Assert.IsTrue(controller.getUDPClient().getErrorState(), "Drone not in error state.");
			
			controller.getUDPClient().setErrorState(false);
			controller.getUDPClient().startConnection();
			//Assert.IsTrue(controller.getUDPClient().getErrorState(), "Drone not in error state.");
			
			controller.stop();
			simulator.stop();
		}
	}
}