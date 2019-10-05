using NUnit.Framework;
using Shared;
using Shared.MessageTypes;

namespace UnitTests
{
	[TestFixture]
	public class DroneStateTests
	{
		[Test]
		public void Main()
		{
			DroneState state = new DroneState();
			
			state.travelTo(500, 0, 0);
			Assert.AreNotEqual(state.getPositionX(), 500);
			
			if (!state.isInCommandMode())
			{
				state.setInCommandMode(true);
			}
			
			Status s = new Status(TestingUtils.defaultState);
			
			state.updateFlyingInfo(s);
			
			state.setInCommandMode(false);
			Assert.IsFalse(state.isInCommandMode());
		}
	}
}