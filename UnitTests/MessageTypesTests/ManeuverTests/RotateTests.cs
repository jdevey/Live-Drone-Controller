using NUnit.Framework;
using Shared;
using Shared.MessageTypes.Maneuvers;

namespace UnitTests.MessageTypesTests.ManeuverTests
{
	[TestFixture]
	public class RotateTests
	{
		[Test]
		public void Main()
		{
			Rotate r1 = new Rotate("cw 90");
			Rotate r2 = new Rotate("ccw 45");

			DroneState state = new DroneState();
			state.setInCommandMode(true);
			state.setHasTakenOff(true);
			r1.updateState(state);
			Assert.AreEqual(state.getOrientation(), 90);
			r2.updateState(state);
			Assert.AreEqual(state.getOrientation(), 45);
		}
	}
}