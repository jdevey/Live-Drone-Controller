using NUnit.Framework;
using Shared;
using Shared.MessageTypes.Maneuvers;

namespace UnitTests.MessageTypesTests.ManeuverTests
{
	[TestFixture]
	public class CurveTests
	{
		[Test]
		public void Main()
		{
			Curve c = new Curve("curve 0 0 0 20 30 40 10");
			DroneState state = new DroneState();
			state.setInCommandMode(true);
			state.setHasTakenOff(true);
			c.updateState(state);
			Assert.AreEqual(state.getPositionX(), 20);
			Assert.AreEqual(state.getPositionY(), 30);
			Assert.AreEqual(state.getPositionZ(), 40);
		}
	}
}