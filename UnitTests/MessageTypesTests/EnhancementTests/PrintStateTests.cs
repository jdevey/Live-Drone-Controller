using NUnit.Framework;
using Shared;
using Shared.MessageTypes.Enhancements;

namespace UnitTests.MessageTypesTests.EnhancementTests
{
	[TestFixture]
	public class PrintStateTests
	{
		[Test]
		public void Main()
		{
			PrintState pbct = new PrintState(PrintState.getKeyword());
			DroneState state = new DroneState();
			pbct.activate(state);
		}
	}
}