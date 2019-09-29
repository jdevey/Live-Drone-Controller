using System.Collections.Generic;
using System.ComponentModel.Design;

namespace Shared
{
	// We need to encapsulate every type of message here. That means that every message type has a group,
	// and each message's string representation, expected response, and number of arguments can be found
	// here.
	public static class MessageConstants
	{
		public static readonly HashSet<string> SETUP_COMMANDS = new HashSet<string>
		{
			"command",
			"takeoff",
			"land"
		};
		
		public static readonly HashSet<string> MANEUVERS = new HashSet<string>
		{
			"up",
			"down",
			"left",
			"right",
			"forward",
			"back",
			"cw",
			"ccw",
			"flip",
			"go",
			"curve",
			"jump"
		};
	}
}