using NUnit.Framework;
using Shared;
using Shared.MessageTypes.Enhancements;

namespace UnitTests.MessageTypesTests.EnhancementTests
{
	[TestFixture]
	public class PrintBroadcastCountTests
	{
		[Test]
		public void Main()
		{
			PrintBroadcastCount pbct = new PrintBroadcastCount(PrintBroadcastCount.getKeyword());
			DroneState state = new DroneState();
			pbct.activate(state);
		}
	}
}