using System.Collections.Generic;

namespace Shared.MessageTypes.Maneuvers
{
	public class DirectionalMove : ManeuverBase
	{
		public DirectionalMove(string text) : base(text)
		{
		}

		public static HashSet<string> getKeywords()
		{
			return new HashSet<string>
			{
				"forward",
				"backward",
				"left",
				"right",
				"up",
				"down"
			};
		}
	}
}