using NUnit.Framework;
using Shared;
using Shared.MessageTypes.Enhancements;

namespace UnitTests.MessageTypesTests.EnhancementTests
{
	[TestFixture]
	public class SleepActionTests
	{
		[Test]
		public void Main()
		{
			SleepAction pbct = new SleepAction(SleepAction.getKeyword() + " 10");
			DroneState state = new DroneState();
			pbct.activate();
		}
	}
}