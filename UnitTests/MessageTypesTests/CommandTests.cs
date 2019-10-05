using NUnit.Framework;
using Shared.MessageTypes;

namespace UnitTests.MessageTypesTests
{
	[TestFixture]
	public class CommandTests
	{
		[Test]
		public void Main()
		{
			Command cmd = new Command(Command.getKeyword());
			Assert.AreEqual("command", Command.getKeyword());
		}
	}
}