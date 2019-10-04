using System.Collections.Generic;
using DroneController;
using NUnit.Framework;
using Shared;

namespace UnitTests
{
	[TestFixture]
	public class ErrorStateTests
	{
		[Test]
		public void Main()
		{
			Simulator.Simulator simulator = TestingUtils.generateSim(TestingUtils.ports[3]);
			simulator.start();
			Controller controller = TestingUtils.generateContr(TestingUtils.ports[3]);
			controller.getUDPClient().startConnection();

			Assert.IsFalse(simulator.getErrorState(), "Simulator in error state.");
			Assert.IsFalse(controller.getErrorState(), "Controller in error state.");
			
			controller.setErrorState(true);
			Assert.IsTrue(controller.getErrorState(), "Controller not in error state");
			
			controller.stop();
			simulator.stop();
		}
	}
}