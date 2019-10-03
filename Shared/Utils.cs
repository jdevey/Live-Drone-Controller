using System;
using System.Collections.Generic;
using System.Text;
using Shared.MessageTypes;
using Shared.MessageTypes.Maneuvers;

namespace Shared
{
	public static class Utils
	{
		public static string decodeBytes(byte[] bytes)
		{
			return Encoding.UTF8.GetString(bytes);
		}

		public static byte[] encodeString(string s)
		{
			return Encoding.UTF8.GetBytes(s);
		}
		
		public static string formatDouble(double value) {
			return value.ToString("0.00");
		}

		public static string pythagorean3D(int x, int y, int z)
		{
			return Math.Sqrt(x * x + y * y + z * z).ToString("0.00");
		}

		// Split into ints
		public static List<int> splitStringToInts (string s, uint expectedLength)
		{
			List<int> ints = new List<int>();
			string[] segs = s.Split(' ');
			for (int i = 1; i < segs.Length; ++i)
			{
				try
				{
					ints.Add(int.Parse(segs[i]));
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					throw;
				}
			}

			while (ints.Count < expectedLength)
			{
				ints.Add(0);
			}

			return ints;
		}
		
		// Split into doubles
		public static List<double> splitStringToDoubles (string s, uint expectedLength)
		{
			List<double> doubles = new List<double>();
			string[] segs = s.Split(' ');
			for (int i = 1; i < segs.Length; ++i)
			{
				try
				{
					doubles.Add(double.Parse(segs[i]));
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					throw;
				}
			}

			while (doubles.Count < expectedLength)
			{
				doubles.Add(0.0);
			}

			return doubles;
		}
	}
}