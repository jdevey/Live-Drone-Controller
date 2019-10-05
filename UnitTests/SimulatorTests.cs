using DroneController;
using NUnit.Framework;
using Shared.MessageTypes;
using Shared.MessageTypes.Queries;

namespace UnitTests
{
	[TestFixture]
	public class SimulatorTests
	{
		[Test]
		public void Main()
		{
			Simulator.Simulator simulator = TestingUtils.generateSim(TestingUtils.ports[4]);
			simulator.start();
			Controller controller = TestingUtils.generateContr(TestingUtils.ports[4]);
			controller.getUDPClient().startConnection();

			Assert.IsFalse(simulator.getErrorState(), "Simulator in error state.");
			Assert.IsFalse(controller.getErrorState(), "Controller in error state.");
			
			controller.getUDPClient().sendMessage(Command.getKeyword(),
				controller.getUDPClient().getDroneCommClient(), controller.getUDPClient().getCommandIpEndPoint());
			
			controller.getUDPClient().sendMessage(TimeQuery.getKeyword(),
				controller.getUDPClient().getDroneCommClient(), controller.getUDPClient().getCommandIpEndPoint());
			
			controller.getUDPClient().sendMessage(SpeedQuery.getKeyword(),
				controller.getUDPClient().getDroneCommClient(), controller.getUDPClient().getCommandIpEndPoint());
			
			controller.getUDPClient().sendMessage(BatteryQuery.getKeyword(),
				controller.getUDPClient().getDroneCommClient(), controller.getUDPClient().getCommandIpEndPoint());
			
			Assert.IsFalse(controller.getUDPClient().getErrorState());
			
			controller.getUDPClient().sendMessage("invalid message!",
				controller.getUDPClient().getDroneCommClient(), controller.getUDPClient().getCommandIpEndPoint());
			
			Assert.IsFalse(controller.getUDPClient().getErrorState());
			
			controller.stop();
			simulator.stop();
		}
	}
}