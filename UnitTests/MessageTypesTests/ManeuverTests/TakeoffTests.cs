using NUnit.Framework;
using Shared;
using Shared.MessageTypes.Maneuvers;

namespace UnitTests.MessageTypesTests.ManeuverTests
{
	[TestFixture]
	public class TakeoffTests
	{
		[Test]
		public void Main()
		{
			Takeoff f = new Takeoff (Takeoff.getKeyword());

			DroneState state = new DroneState();
			state.setInCommandMode(true);

			f.updateState(state);
			Assert.AreEqual(state.getPositionZ(), 100);
			Assert.AreEqual(state.hasTakenOff(), true);
		}
	}
}