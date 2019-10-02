using System;
using System.Collections.Generic;

namespace Shared.MessageTypes
{
	public class Status : Message
	{
		private int pitch;
		private int roll;
		private int yaw;
		private int speedX;
		private int speedY;
		private int speedZ;
		private int lowTemperature;
		private int highTemperature;
		private int flightDistance;
		private int height;
		private int batteryPercentage;
		private double barometerMeasurement;
		private int motorTime;
		private double accelerationX;
		private double accelerationY;
		private double accelerationZ;
		

//		public Status(int pitch, int roll, int yaw, int speedX, int speedY, int speedZ,
//			int lowTemperature, int highTemperature, int flightDistance, int height,
//			int batteryPercentage, double barometerMeasurement, int motorTime,
//			double accelerationX, double accelerationY, double accelerationZ)
public Status(DroneState state) : base(getMessageTextFromState(state))
		{
			pitch = state.getPitch();
			roll = state.getRoll();
			yaw = state.getYaw();
			speedX = state.getSpeedX();
			speedY = state.getSpeedY();
			speedZ = state.getSpeedZ();
			lowTemperature = state.getLowTemperature();
			highTemperature = state.getHighTemperature();
			flightDistance = state.getFlightDistance();
			height = state.getHeight();
			batteryPercentage = state.getBatteryPercentage();
			barometerMeasurement = state.getBarometerMeasurement();
			motorTime = state.getMotorTime();
			accelerationX = state.getAccelerationX();
			accelerationY = state.getAccelerationY();
			accelerationZ = state.getAccelerationZ();
		}

		public Status(string data) : base(data)
		{
			parseData(data);
		}
		
		public static string getKeyword()
		{
			return "mid";
		}

		public static string formatStringForMessage(int pitch, int roll, int yaw, int speedX,
			int speedY, int speedZ, int lowTemperature, int highTemperature, int flightDistance,
			int height, int batteryPercentage, double barometerMeasurement, int motorTime,
			double accelerationX, double accelerationY, double accelerationZ)
		{
			return string.Format("mid:-1;x:0;y:0;z:0;mpry:0,0,0;pitch:{0};roll:{1};yaw:{2};" +
			                     "vgx:{3};vgy:{4};vgz:{5};" +
			                     "templ:{6};temph:{7};" +
			                     "tof:{8};h:{9};" +
			                     "bat:{10};baro:{11};" +
			                     "time:{12};" +
			                     "agx:{13};agy:{14};agz:{15}",
				pitch, roll, yaw,
				speedX, speedY, speedZ,
				lowTemperature, highTemperature,
				flightDistance, height,
				batteryPercentage, barometerMeasurement,
				motorTime,
				accelerationX, accelerationY, accelerationZ);
		}

		public static string getMessageTextFromState(DroneState state)
		{
			return formatStringForMessage(
				state.getPitch(), state.getRoll(), state.getYaw(),
				state.getSpeedX(), state.getSpeedY(), state.getSpeedZ(),
				state.getLowTemperature(), state.getHighTemperature(),
				state.getFlightDistance(), state.getHeight(),
				state.getBatteryPercentage(), state.getBarometerMeasurement(),
				state.getMotorTime(), state.getAccelerationX(),
				state.getAccelerationY(), state.getAccelerationZ()
			);
		}

		public string getMessageText()
		{
			return formatStringForMessage(
				pitch, roll, yaw,
				speedX, speedY, speedZ,
				lowTemperature, highTemperature,
				flightDistance, height,
				batteryPercentage, barometerMeasurement,
				motorTime,
				accelerationX, accelerationY, accelerationZ
			);
		}

//		public override string getMessageType()
//		{
//			return "status";
//		}

		public int getPitch()
		{
			return pitch;
		}

		public int getRoll()
		{
			return roll;
		}

		public int getYaw()
		{
			return yaw;
		}

		public int getSpeedX()
		{
			return speedX;
		}

		public int getSpeedY()
		{
			return speedY;
		}

		public int getSpeedZ()
		{
			return speedZ;
		}

		public double getAccelerationX()
		{
			return accelerationX;
		}

		public double getAccelerationY()
		{
			return accelerationY;
		}

		public double getAccelerationZ()
		{
			return accelerationZ;
		}

		public int getLowTemperature()
		{
			return lowTemperature;
		}

		public int getHighTemperature()
		{
			return highTemperature;
		}

		public int getFlightDistance()
		{
			return flightDistance;
		}

		public int getHeight()
		{
			return height;
		}

		public int getBatteryPercentage()
		{
			return batteryPercentage;
		}

		public double getBarometerMeasurement()
		{
			return barometerMeasurement;
		}

		public int getMotorTime()
		{
			return motorTime;
		}

		private void parseData(string data)
		{
			if (String.IsNullOrEmpty(data))
				return;

			string[] stateFields = data.Trim().Split(';');
			if (stateFields.Length != 21)
			{
				return;
			}

			pitch = parseInteger("pitch", stateFields[5]);
			roll = parseInteger("roll", stateFields[6]);
			yaw = parseInteger("yaw", stateFields[7]);
			speedX = parseInteger("vgx", stateFields[8]);
			speedY = parseInteger("vgy", stateFields[9]);
			speedZ = parseInteger("vgz", stateFields[10]);
			lowTemperature = parseInteger("templ", stateFields[11]);
			highTemperature = parseInteger("temph", stateFields[12]);
			flightDistance = parseInteger("tof", stateFields[13]);
			height = parseInteger("h", stateFields[14]);
			batteryPercentage = parseInteger("bat", stateFields[15]);
			barometerMeasurement = parseDouble("baro", stateFields[16]);
			motorTime = parseInteger("time", stateFields[17]);
			accelerationX = parseDouble("agx", stateFields[18]);
			accelerationY = parseDouble("agy", stateFields[19]);
			accelerationZ = parseDouble("agz", stateFields[20]);
		}

		private int parseInteger(string expectedLabel, string data)
		{
			string valueToParse = getValueToParse(expectedLabel, data);
			if (valueToParse == null)
				return 0;

			int result = 0;
			try
			{
				result = int.Parse(valueToParse);
			}
			catch (Exception e)
			{
				Console.WriteLine("ERROR: Failed to parse to integer in Status class.");
				Console.WriteLine(e.Message);
			}

			return result;
		}

		private double parseDouble(string expectedLabel, string data)
		{
			string valueToParse = getValueToParse(expectedLabel, data);
			if (valueToParse == null)
				return 0.0;

			double result = 0.0;
			try
			{
				result = double.Parse(valueToParse);
			}
			catch (Exception e)
			{
				Console.WriteLine("ERROR: Failed to parse to double in Status class.");
				Console.WriteLine(e.Message);
			}

			return result;
		}

		private string getValueToParse(string expectedLabel, string data)
		{
			if (String.IsNullOrEmpty(expectedLabel) || String.IsNullOrEmpty(data))
				return null;

			string[] parts = data.Split(':');
			if (parts.Length != 2)
				return null;

			if (parts[0].Trim() != expectedLabel)
				return null;

			return parts[1].Trim();
		}
	}
}