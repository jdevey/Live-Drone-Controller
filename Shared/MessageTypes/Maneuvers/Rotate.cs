using System.Collections.Generic;

namespace Shared.MessageTypes.Maneuvers
{
	public class Rotate : ManeuverBase
	{
		private static readonly string CW = "cw";
		private static readonly string CCW = "ccw";
		
		private static readonly HashSet<string> directionalMoves = new HashSet<string>
		{
			CW,
			CCW
		};

		private int rotation;
		
		public Rotate(string text) : base(text)
		{
			List<int> ints = Utils.splitStringToInts(text, 1);
			if (text.StartsWith(CW))
			{
				rotation = ints[0];
			}
			else if (text.StartsWith(CCW))
			{
				rotation = -ints[0];
			}
		}
		
		public static HashSet<string> getKeywords()
		{
			return new HashSet<string>
			{
				"cw",
				"ccw"
			};
		}
		
		public override void updateState(DroneState state)
		{
			state.rotate(rotation);
		}
	}
}