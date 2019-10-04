using System.Text;
using NUnit.Framework;
using Shared;

namespace UnitTests
{
	[TestFixture]
	public class UtilsTests
	{
		[Test]
		public void Main()
		{
			string msg1 = "foo";
			byte[] encoded = Encoding.UTF8.GetBytes(msg1);
			Assert.AreEqual(msg1, Utils.decodeBytes(encoded));
			Assert.AreEqual(encoded, Utils.encodeString(msg1));
			Assert.AreEqual("0.65", Utils.formatDouble(0.654));
			Assert.AreEqual("6.93", Utils.pythagorean3D(4, 4, 4));
			
		}
	}
}