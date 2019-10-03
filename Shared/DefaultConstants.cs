namespace Shared
{
	public static class DefaultConstants
	{
		public const string LOCALHOST = "127.0.0.1";
		public const string DEFAULT_DRONE_IP = LOCALHOST;
		public const int DEFAULT_COMMAND_PORT = 8889;
		public const int DEFAULT_TELLO_STATE_PORT = 8890;
		public const int DEFAULT_LOCAL_PORT = 8891;
		public const int DEFAULT_TIMEOUT = 2000; // ms
		public const uint DEFAULT_MAX_RETRIES = 3;
	}
}