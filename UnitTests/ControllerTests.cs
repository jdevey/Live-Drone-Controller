using DroneController;
using NUnit.Framework;
using Shared;
using Simulator;

namespace UnitTests
{
	[TestFixture]
	public class ControllerTests
	{
		[Test]
		public void Main()
		{
			Simulator.Simulator simulator = new Simulator.Simulator();
			simulator.start();
			Controller controller = new Controller();
			Assert.IsFalse(controller.getUDPClient().getErrorState(), "Drone in error state.");

			Assert.AreEqual(controller.getUDPClient(), controller.getUDPClient());

			controller.addMission(TestConstants.leftRightMission);
			Assert.IsFalse(controller.getErrorState(), "Drone in error state.");

			controller.addMission(TestConstants.badMission2);
			Assert.IsTrue(controller.getErrorState(), "Drone not in error state.");
			controller.setErrorState(false);

			controller.executeMission(1);
			Assert.IsFalse(controller.getErrorState(), "Drone in error state.");

			controller.executeMission(0);
			Assert.IsTrue(controller.getErrorState(), "Drone not in error state.");
			controller.setErrorState(false);

//			controller.executeMission(7);
//			Assert.IsTrue(controller.getErrorState(), "Drone not in error state.");
//			controller.setErrorState(false);

			controller.stop();
			simulator.stop();
			Assert.Pass();
		}

//		Controller controller = new Controller(
//			droneIp: DefaultConstants.LOCALHOST,
//			commandPort: DefaultConstants.DEFAULT_COMMAND_PORT,
//			telloStatePort: DefaultConstants.DEFAULT_TELLO_STATE_PORT,
//			timeout: DefaultConstants.DEFAULT_TIMEOUT,
//			maxRetries: DefaultConstants.DEFAULT_MAX_RETRIES
//		);
	}
}