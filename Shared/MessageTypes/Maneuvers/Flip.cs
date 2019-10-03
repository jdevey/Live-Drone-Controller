using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Shared.MessageTypes.Maneuvers
{
	public class Flip : ManeuverBase
	{
		private static readonly HashSet<char> validDirections = new HashSet<char>
		{
			'l',
			'r',
			'f',
			'b'
		};
		
		private readonly char direction;
		public Flip(string text) : base(text)
		{
			if (!validDirections.Contains(text[5]))
			{
				Console.WriteLine("ERROR: Invalid direction specified in flip.");
				direction = 'f';
			}
			else
			{
				direction = text[5];
			}
		}
		
		public static string getKeyword()
		{
			return "flip";
		}
		
		public override void updateState(DroneState state)
		{
			switch (direction)
			{
				case 'r':
					state.move(10, 0, 0);
					break;
				case 'l':
					state.move(-10, 0, 0);
					break;
				case 'f':
					state.move(10, 0, 0);
					break;
				case 'b':
					state.move(-10, 0, 0);
					break;
			}
		}

		public char getDirection()
		{
			return direction;
		}
	}
}