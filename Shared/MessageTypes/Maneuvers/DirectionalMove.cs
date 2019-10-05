using System.Collections.Generic;

namespace Shared.MessageTypes.Maneuvers
{
	public class DirectionalMove : ManeuverBase
	{
		private static readonly string FORWARD = "forward";
		private static readonly string BACK = "back";
		private static readonly string RIGHT = "right";
		private static readonly string LEFT = "left";
		private static readonly string UP = "up";
		private static readonly string DOWN = "down";
		
		private static readonly HashSet<string> directionalMoves = new HashSet<string>
		{
			FORWARD,
			BACK,
			RIGHT,
			LEFT,
			UP,
			DOWN
		};
		
		private readonly int dx = 0;
		private readonly int dy = 0;
		private readonly int dz = 0;
		public DirectionalMove(string text) : base(text)
		{
			List<int> ints = Utils.splitStringToInts(text, 1);
			if (text.StartsWith(FORWARD))
			{
				dy = ints[0];
			}
			else if (text.StartsWith(BACK))
			{
				dy = -ints[0];
			}
			else if (text.StartsWith(RIGHT))
			{
				dx = ints[0];
			}
			else if (text.StartsWith(LEFT))
			{
				dx = -ints[0];
			}
			else if (text.StartsWith(UP))
			{
				dz = ints[0];
			}
			else if (text.StartsWith(DOWN))
			{
				dz = -ints[0];
			}
		}

		public static HashSet<string> getKeywords()
		{
			return directionalMoves;
		}
		
		public override void updateState(DroneState state)
		{
			state.move(dx, dy, dz);
		}

		public int getX()
		{
			return dx;
		}

		public int getY()
		{
			return dy;
		}

		public int getZ()
		{
			return dz;
		}
	}
}