using NUnit.Framework;
using Shared;
using Shared.MessageTypes.Responses;

namespace UnitTests.MessageTypesTests.ResponseTests
{
	[TestFixture]
	public class DataResponseTests
	{
		[Test]
		public void Main()
		{
			DataResponse f = new DataResponse("20");

			Assert.AreEqual(f.getMessageText(), "20");
		}
	}
}