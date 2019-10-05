using NUnit.Framework;
using Shared;
using Shared.MessageTypes.Maneuvers;

namespace UnitTests.MessageTypesTests.ManeuverTests
{
	[TestFixture]
	public class GoTests
	{
		[Test]
		public void Main()
		{
			Go f = new Go("go 20 30 40 10");

			DroneState state = new DroneState();
			state.setInCommandMode(true);
			state.setHasTakenOff(true);
			
			f.updateState(state);
			Assert.AreEqual(state.getPositionX(), 20);
			Assert.AreEqual(state.getPositionY(), 30);
			Assert.AreEqual(state.getPositionZ(), 40);
		}
	}
}