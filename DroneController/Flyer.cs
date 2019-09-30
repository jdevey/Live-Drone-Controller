using System;

namespace DroneController
{
	internal class Flyer
	{
		public static void Main(string[] args)
		{
			string ip = "127.0.0.1";
			int remotePort = 8889;
			int localPort = 8890;
			DroneComm home = new DroneComm(ip, remotePort, localPort, true);
			DroneComm remote = new DroneComm(ip, localPort, remotePort, false);
			
			remote.start();
			
			home.sendMessage("command");
			string rcvd = home.getResponse();
			
			Console.WriteLine(rcvd);
		}
	}
}