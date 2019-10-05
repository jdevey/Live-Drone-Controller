using System.Threading;
using DroneController;
using NUnit.Framework;
using Shared;
using Shared.MessageTypes.Queries;

namespace UnitTests.MessageTypesTests.QueryTests
{
	[TestFixture]
	public class BatteryQueryTests
	{
		[Test]
		public void Main()
		{
			Simulator.Simulator simulator = TestingUtils.generateSim(TestingUtils.ports[8]);
			simulator.start();
			Controller controller = TestingUtils.generateContr(TestingUtils.ports[8]);
			controller.getUDPClient().startConnection();

			Assert.IsFalse(simulator.getErrorState(), "Simulator in error state.");
			Assert.IsFalse(controller.getErrorState(), "Controller in error state.");
			
			BatteryQuery f = new BatteryQuery ("battery?");

			f.activate(controller.getUDPClient().getDroneCommClient(),
				controller.getUDPClient().getCommandIpEndPoint());

			int resp = int.Parse(controller.getUDPClient().getResponse(
				controller.getUDPClient().getDroneCommClient(), controller.getUDPClient().getCommandIpEndPoint()));		
			
			Assert.IsTrue(resp > 60);
			
			controller.stop();
			simulator.stop();
		}
	}
}