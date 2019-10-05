using NUnit.Framework;
using Shared;
using Shared.MessageTypes.Maneuvers;

namespace UnitTests.MessageTypesTests.ManeuverTests
{
	[TestFixture]
	public class FlipTests
	{
		[Test]
		public void Main()
		{
			Flip f = new Flip("flip f");
			Flip b = new Flip("flip b");
			Flip r = new Flip("flip r");
			Flip l = new Flip("flip l");
			
			DroneState state = new DroneState();
			state.setInCommandMode(true);
			state.setHasTakenOff(true);
			
			f.updateState(state);
			Assert.AreEqual(state.getPositionY(), 10);
			b.updateState(state);
			Assert.AreEqual(state.getPositionY(), 0);
			r.updateState(state);
			Assert.AreEqual(state.getPositionX(), 10);
			l.updateState(state);
			Assert.AreEqual(state.getPositionX(), 0);
		}
	}
}