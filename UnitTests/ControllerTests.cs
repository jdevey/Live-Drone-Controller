using System.Collections.ObjectModel;
using System.Threading;
using DroneController;
using NUnit.Framework;
using Shared;
using Simulator;

namespace UnitTests
{
	[TestFixture, NonParallelizable, SingleThreaded]
	public class ControllerTests
	{
		[Test, OneTimeTearDown, NonParallelizable]
		public void Main()
		{
			Simulator.Simulator simulator = TestingUtils.generateSim(TestingUtils.ports[1]);
//			Simulator.Simulator simulator = new Simulator.Simulator();
			Assert.IsFalse(simulator.getSimulatorComm().getErrorState(), "Drone in error state at point 0.");
			simulator.start();
			Controller controller = TestingUtils.generateContr(TestingUtils.ports[1]);
//			Controller controller = new Controller(localPort: 8897);
			Assert.IsFalse(controller.getUDPClient().getErrorState(), "Drone in error state at point 1.");
			controller.getUDPClient().startConnection();
			Assert.IsFalse(controller.getUDPClient().getErrorState(), "Drone in error state at point 2.");

			Assert.NotNull(controller.getUDPClient());

			controller.addMission(TestConstants.leftRightMission);
			Assert.IsFalse(controller.getErrorState(), "Drone in error state at point 3.");

			controller.addMission(TestConstants.badMission2);
			Assert.IsTrue(controller.getErrorState(), "Drone not in error state.");
			controller.setErrorState(false);

			controller.executeMission(1);
			Assert.IsFalse(controller.getErrorState(), "Drone in error state at point 4.");

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