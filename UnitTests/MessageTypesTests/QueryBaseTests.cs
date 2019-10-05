using DroneController;
using NUnit.Framework;
using Shared.MessageTypes;
using Shared.MessageTypes.Queries;

namespace UnitTests.MessageTypesTests
{
	[TestFixture]
	public class QueryBaseTests
	{
		[Test]
		public void Main()
		{
			Simulator.Simulator simulator = TestingUtils.generateSim(TestingUtils.ports[7]);
			simulator.start();
			Controller controller = TestingUtils.generateContr(TestingUtils.ports[7]);
			controller.getUDPClient().startConnection();

			Assert.IsFalse(simulator.getErrorState(), "Simulator in error state.");
			Assert.IsFalse(controller.getErrorState(), "Controller in error state.");
			
			QueryBase qb = new QueryBase(BatteryQuery.getKeyword());
			qb.activate(controller.getUDPClient().getDroneCommClient(),
				controller.getUDPClient().getCommandIpEndPoint());
			
			Assert.IsFalse(controller.getUDPClient().getErrorState());
			
			controller.stop();
			simulator.stop();
		}
	}
}