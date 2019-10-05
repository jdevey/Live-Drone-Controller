using System.Net;
using DroneController;
using NUnit.Framework;
using Shared;
using Shared.MessageTypes.Responses;

namespace UnitTests
{
	[TestFixture]
	public class UDPCommBase
	{
		[Test]
		public void Main()
		{
			Simulator.Simulator simulator = TestingUtils.generateSim(TestingUtils.ports[0]);
			simulator.start();
			Controller controller = TestingUtils.generateContr(TestingUtils.ports[0]);
			controller.getUDPClient().startConnection();

			Assert.IsFalse(simulator.getErrorState(), "Simulator in error state.");
			Assert.IsFalse(controller.getErrorState(), "Controller in error state.");

//			IPEndPoint fakeIpEndPoint = new IPEndPoint(IPAddress.Parse("123.0.0.78"), 8665);
//			controller.getUDPClient().getResponse(controller.getUDPClient().getDroneCommClient(), fakeIpEndPoint);

			simulator.getSimulatorComm().sendMessage(Error.getKeyword(),
				simulator.getSimulatorComm().getUdpClient(), simulator.getSimulatorComm().getLocalIpEndPoint());
			
			controller.stop();
			simulator.stop();
			
//			for (uint i = 0; i < 4; ++i)
//			{
//				simulator.getSimulatorComm().sendMessage("",
//					simulator.getSimulatorComm().getUdpClient(), simulator.getSimulatorComm().getLocalIpEndPoint());
//			}
		}
	}
}