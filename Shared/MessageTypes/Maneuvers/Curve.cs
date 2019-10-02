using System.Collections.Generic;

namespace Shared.MessageTypes.Maneuvers
{
	public class Curve : ManeuverBase
	{
		private readonly int x1 = 0;
		private readonly int y1 = 0;
		private readonly int z1 = 0;
		private readonly int x2 = 0;
		private readonly int y2 = 0;
		private readonly int z2 = 0;
		private readonly int speed = 0;
		public Curve(string text) : base(text)
		{
			List<int> ints = Utils.splitStringToInts(text, 7);
			x1 = ints[0];
			y1 = ints[1];
			z1 = ints[2];
			x2 = ints[3];
			y2 = ints[4];
			z2 = ints[5];
			speed = ints[6];
		}
		
		public static string getKeyword()
		{
			return "curve";
		}

		public override void updateState(DroneState state)
		{
			state.travelTo(x2, y2, z2);
			// TODO sleep?
		}
	}
}