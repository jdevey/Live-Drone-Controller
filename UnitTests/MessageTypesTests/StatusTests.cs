using NUnit.Framework;
using Shared;
using Shared.MessageTypes;

namespace UnitTests.MessageTypesTests
{
	[TestFixture]
	public class StatusTests
	{
		[Test]
		public void Main()
		{
			DroneState state = new DroneState();
			
			Status status = new Status(state);
			
			Assert.AreEqual(TestingUtils.defaultState, status.getMessageText());
			
			Status statusEmpty = new Status("");
			Status statusIncomplete = new Status("incomplete;data");
			
			Assert.AreEqual(status.getPitch(), 0);
			Assert.AreEqual(status.getRoll(), 0);
			Assert.AreEqual(status.getYaw(), 0);
			Assert.AreEqual(status.getSpeedX(), 0);
			Assert.AreEqual(status.getSpeedY(), 0);
			Assert.AreEqual(status.getSpeedZ(), 0);
			Assert.AreEqual(status.getAccelerationX(), 0);
			Assert.AreEqual(status.getAccelerationY(), 0);
			Assert.AreEqual(status.getAccelerationZ(), 0);
			Assert.AreEqual(status.getLowTemperature(), 20);
			Assert.AreEqual(status.getHighTemperature(), 20);
			Assert.AreEqual(status.getFlightDistance(), 0);
			Assert.AreEqual(status.getHeight(), 0);
			Assert.AreEqual(status.getBatteryPercentage(), 100);
			Assert.AreEqual(status.getBarometerMeasurement(), 0);
			Assert.AreEqual(status.getMotorTime(), 0);
			
		}
	}
}