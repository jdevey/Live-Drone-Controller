using System.Threading;
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
			Assert.IsFalse(simulator.getSimulatorComm().getErrorState(), "Drone in error state -1.");
			simulator.start();
			Controller controller = new Controller();
			Assert.IsFalse(controller.getUDPClient().getErrorState(), "Drone in error state 0.");
			controller.getUDPClient().startConnection();
			Assert.IsFalse(controller.getUDPClient().getErrorState(), "Drone in error state 1.");

			Assert.NotNull(controller.getUDPClient());

			controller.addMission(TestConstants.leftRightMission);
			Assert.IsFalse(controller.getErrorState(), "Drone in error state 2.");

			controller.addMission(TestConstants.badMission2);
			Assert.IsTrue(controller.getErrorState(), "Drone not in error state.");
			controller.setErrorState(false);

			controller.executeMission(1);
			Assert.IsFalse(controller.getErrorState(), "Drone in error state 3.");

			controller.executeMission(0);
			Assert.IsTrue(controller.getErrorState(), "Drone not in error state.");
			controller.setErrorState(false);

//			controller.executeMission(7);
//			Assert.IsTrue(controller.getErrorState(), "Drone not in error state.");
//			controller.setErrorState(false);

			controller.stop();
			simulator.stop();
		}
	}
}