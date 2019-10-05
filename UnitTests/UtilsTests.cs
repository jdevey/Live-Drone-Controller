using System.Collections.Generic;
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

			List<int> expected1 = new List <int> {1, 2, 3};
			Assert.AreEqual(expected1, Utils.splitStringToInts("x 1 2 3", 3));
			
			List<int> expected2 = new List <int> {1, 2, 3, 0};
			Assert.AreEqual(expected2, Utils.splitStringToInts("x 1 2 3", 4));
			
			List<double> expected3 = new List <double> {1.0, 2.0, 3.0};
			Assert.AreEqual(expected3, Utils.splitStringToDoubles("x 1.0 2.0 3.0", 3));
			
			List<double> expected4 = new List <double> {1.0, 2.0, 3.0, 0.0};
			Assert.AreEqual(expected4, Utils.splitStringToDoubles("x 1.0 2.0 3.0", 4));
			
			Assert.AreEqual(new List<int>(), Utils.splitStringToInts("f f", 1));
			Assert.AreEqual(new List<double>(), Utils.splitStringToDoubles("f f", 1));
		}
	}
}