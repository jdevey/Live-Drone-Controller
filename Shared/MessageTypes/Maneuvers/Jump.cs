using System.Collections.Generic;

namespace Shared.MessageTypes.Maneuvers
{
	public class Jump : ManeuverBase
	{
		private readonly int x = 0;
		private readonly int y = 0;
		private readonly int z = 0;
		private readonly int speed = 0;
		public Jump(string text) : base(text)
		{
			List<int> ints = Utils.splitStringToInts(text, 4);
			x = ints[0];
			y = ints[1];
			z = ints[2];
			speed = ints[3];
		}
		
		public static string getKeyword()
		{
			return "jump";
		}
		
		public override void updateState(DroneState state)
		{
			state.move(x, y, z);
		}
	}
}