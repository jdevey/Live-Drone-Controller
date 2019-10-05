using NUnit.Framework;
using Shared;
using Shared.MessageTypes.Maneuvers;

namespace UnitTests.MessageTypesTests.ManeuverTests
{
	[TestFixture]
	public class DirectionalMoveTests
	{
		[Test]
		public void Main()
		{
			DirectionalMove c = new DirectionalMove("up 20");
			Assert.AreEqual(c.getZ(), 20);
			
			DroneState state = new DroneState();
			state.setInCommandMode(true);
			state.setHasTakenOff(true);
			c.updateState(state);
			Assert.AreEqual(state.getPositionZ(), 20);
			
			DirectionalMove c2 = new DirectionalMove("down 20");
			c2.updateState(state);
			Assert.AreEqual(state.getPositionZ(), 0);
		}
	}
}