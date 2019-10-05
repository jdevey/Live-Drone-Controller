using NUnit.Framework;
using Shared;
using Shared.MessageTypes.Maneuvers;

namespace UnitTests.MessageTypesTests.ManeuverTests
{
	[TestFixture]
	public class LandTests
	{
		[Test]
		public void Main()
		{
			Land f = new Land (Land.getKeyword());

			DroneState state = new DroneState();
			state.setInCommandMode(true);
			state.setHasTakenOff(true);
			state.move(0, 0, 200);
			
			f.updateState(state);
			Assert.AreEqual(state.getPositionZ(), 0);
			Assert.AreEqual(state.hasTakenOff(), false);
		}
	}
}