using DroneController;
using NUnit.Framework;
using Shared.MessageTypes.Queries;

namespace UnitTests.MessageTypesTests.QueryTests
{
	[TestFixture]
	public class TimeQueryTests
	{
		[Test]
		public void Main()
		{
			Simulator.Simulator simulator = TestingUtils.generateSim(TestingUtils.ports[10]);
			simulator.start();
			Controller controller = TestingUtils.generateContr(TestingUtils.ports[10]);
			controller.getUDPClient().startConnection();

			Assert.IsFalse(simulator.getErrorState(), "Simulator in error state.");
			Assert.IsFalse(controller.getErrorState(), "Controller in error state.");
			
			TimeQuery f = new TimeQuery ("time?");

			f.activate(controller.getUDPClient().getDroneCommClient(),
				controller.getUDPClient().getCommandIpEndPoint());

			string s = controller.getUDPClient().getResponse(
				controller.getUDPClient().getDroneCommClient(), controller.getUDPClient().getCommandIpEndPoint());
			double resp = double.Parse(s);
			
			Assert.IsTrue(resp < 1000.0);
			
			controller.stop();
			simulator.stop();
		}
	}
}