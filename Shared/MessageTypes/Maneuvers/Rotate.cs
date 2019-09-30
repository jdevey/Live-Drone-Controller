using System.Collections.Generic;

namespace Shared.MessageTypes.Maneuvers
{
	public class Rotate : ManeuverBase
	{
		public Rotate(string text) : base(text)
		{
		}
		
		public static HashSet<string> getKeywords()
		{
			return new HashSet<string>
			{
				"cw",
				"ccw"
			};
		}
	}
}