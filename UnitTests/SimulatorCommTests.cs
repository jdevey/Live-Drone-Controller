using DroneController;
using NUnit.Framework;

namespace UnitTests
{
	[TestFixture]
	public class SimulatorCommTests
	{
		[Test]
		public void Main()
		{
			Simulator.Simulator simulator = TestingUtils.generateSim(TestingUtils.ports[5]);
			simulator.start();
			Controller controller = TestingUtils.generateContr(TestingUtils.ports[5]);
			controller.getUDPClient().startConnection();

			Assert.IsFalse(simulator.getErrorState(), "Simulator in error state.");
			Assert.IsFalse(controller.getErrorState(), "Controller in error state.");
			
			Simulator.Simulator simController = TestingUtils.generateSim(TestingUtils.ports[5]);
			Assert.IsTrue(simController.getSimulatorComm().getErrorState(), "Duplicate controller not in error state.");
			simController.getSimulatorComm().setErrorState(false);
//			simController.start();
//			Assert.IsTrue(simController.getSimulatorComm().getErrorState(), "Duplicate controller not in error state.");
			Assert.NotNull(simulator.getSimulatorComm().getCommandIpEndPoint());
			
			controller.stop();
			simulator.stop();
		}
	}
}