using DroneController;
using NUnit.Framework;
using Shared.MessageTypes.Queries;

namespace UnitTests.MessageTypesTests.QueryTests
{
	[TestFixture]
	public class SpeedQueryTests
	{
		[Test]
		public void Main()
		{
			Simulator.Simulator simulator = TestingUtils.generateSim(TestingUtils.ports[9]);
			simulator.start();
			Controller controller = TestingUtils.generateContr(TestingUtils.ports[9]);
			controller.getUDPClient().startConnection();

			Assert.IsFalse(simulator.getErrorState(), "Simulator in error state.");
			Assert.IsFalse(controller.getErrorState(), "Controller in error state.");
			
			SpeedQuery f = new SpeedQuery ("speed?");

			f.activate(controller.getUDPClient().getDroneCommClient(),
				controller.getUDPClient().getCommandIpEndPoint());

			string s = controller.getUDPClient().getResponse(
				controller.getUDPClient().getDroneCommClient(), controller.getUDPClient().getCommandIpEndPoint());
			double resp = double.Parse(s);
			
			Assert.IsTrue(resp == 0.0);
			
			controller.stop();
			simulator.stop();
		}
	}
}