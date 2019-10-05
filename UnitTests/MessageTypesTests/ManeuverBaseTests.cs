using NUnit.Framework;
using Shared;
using Shared.MessageTypes;

namespace UnitTests.MessageTypesTests
{
	[TestFixture]
	public class ManeuverBaseTests
	{
		[Test]
		public void Main()
		{
			ManeuverBase m = new ManeuverBase("");
			DroneState state = new DroneState();
			m.updateState(state);
			Assert.NotNull(state);
		}
	}
}