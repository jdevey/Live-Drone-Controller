using NUnit.Framework;
using Shared.MessageTypes.Responses;

namespace UnitTests.MessageTypesTests.ResponseTests
{
	[TestFixture]
	public class ErrorTests
	{
		[Test]
		public void Main()
		{
			Error f = new Error(Error.getKeyword());

			Assert.AreEqual(Error.getKeyword(), "error");
		}
	}
}