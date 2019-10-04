using System;
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;
using System.Threading;
using DroneController;
using NUnit.Framework;
using Shared;

namespace UnitTests
{
	[TestFixture]
	public class ControllerCommTests
	{
		[Test]
		public void Main()
		{
			Simulator.Simulator simulator = TestingUtils.generateSim(TestingUtils.ports[0]);
			simulator.start();
			Controller controller = TestingUtils.generateContr(TestingUtils.ports[0]);

			controller.getUDPClient().startConnection();
			Assert.IsFalse(controller.getUDPClient().getErrorState(), "Drone in error state.");
			
			Controller duplicateController = TestingUtils.generateContr(TestingUtils.ports[0]);
			Assert.IsTrue(duplicateController.getUDPClient().getErrorState(), "Duplicate controller not in error state.");
			duplicateController.getUDPClient().setErrorState(false);
			duplicateController.getUDPClient().startConnection();
			Assert.IsTrue(duplicateController.getUDPClient().getErrorState(), "Duplicate controller not in error state.");

			Assert.IsNotNull(controller.getUDPClient().getLocalIpEndPoint());
			Assert.IsNotNull(controller.getUDPClient().getTelloStateIpEndPoint());
			
			controller.stop();
			simulator.stop();
		}
	}
}