using System.Collections.Generic;

namespace Shared
{
	public class SetupAction
	{
		private static readonly HashSet<string> KEYWORD_LIST = new HashSet<string>
		{
			"command",
			"takeoff",
			"land"
		};
		
		public static HashSet<string> getKeyWordList()
		{
			return KEYWORD_LIST;
		}
	}
}