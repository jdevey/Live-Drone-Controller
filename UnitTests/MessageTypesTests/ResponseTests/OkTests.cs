using NUnit.Framework;
using Shared.MessageTypes.Responses;

namespace UnitTests.MessageTypesTests.ResponseTests
{
	[TestFixture]
	public class OkTests
	{
		[Test]
		public void Main()
		{
			Ok f = new Ok(Ok.getKeyword());

			Assert.AreEqual(Ok.getKeyword(), "ok");
		}
	}
}