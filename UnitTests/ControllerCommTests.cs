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
			Simulator.Simulator simulator = new Simulator.Simulator();
			simulator.start();
			Controller controller = new Controller(localPort: DefaultConstants.DEFAULT_COMMAND_PORT);
			controller.getUDPClient().startConnection();
			Assert.IsTrue(controller.getUDPClient().getErrorState(), "Drone not in error state.");
			
			controller.getUDPClient().setErrorState(false);
			controller.getUDPClient().startConnection();
			Assert.IsTrue(controller.getUDPClient().getErrorState(), "Drone not in error state.");
			
			controller.stop();
			simulator.stop();
		}
	}
}