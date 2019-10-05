using System;
using System.Collections.Generic;

namespace Shared.MessageTypes
{
	public class Status : Message
	{
		private int x;
		private int y;
		private int z;
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
			x = state.getPositionX();
			y = state.getPositionY();
			z = state.getPositionZ();
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

		public static string formatStringForMessage(int x, int y, int z, int pitch, int roll, int yaw, int speedX,
			int speedY, int speedZ, int lowTemperature, int highTemperature, int flightDistance,
			int height, int batteryPercentage, double barometerMeasurement, int motorTime,
			double accelerationX, double accelerationY, double accelerationZ)
		{
			return string.Format("mid:-1;x:{0};y:{1};z:{2};mpry:0,0,0;pitch:{3};roll:{4};yaw:{5};" +
			                     "vgx:{6};vgy:{7};vgz:{8};" +
			                     "templ:{9};temph:{10};" +
			                     "tof:{11};h:{12};" +
			                     "bat:{13};baro:{14};" +
			                     "time:{15};" +
			                     "agx:{16};agy:{17};agz:{18}",
				x, y, z, pitch, roll, yaw,
				speedX, speedY, speedZ,
				lowTemperature, highTemperature,
				flightDistance, height,
				batteryPercentage, barometerMeasurement,
				motorTime,
				accelerationX, accelerationY, accelerationZ);
		}

		public static string getMessageTextFromState(DroneState state)
		{
			return formatStringForMessage(state.getPositionX(), state.getPositionY(), state.getPositionZ(),
				state.getPitch(), state.getRoll(), state.getYaw(),
				state.getSpeedX(), state.getSpeedY(), state.getSpeedZ(),
				state.getLowTemperature(), state.getHighTemperature(),
				state.getFlightDistance(), state.getHeight(),
				state.getBatteryPercentage(), state.getBarometerMeasurement(),
				state.getMotorTime(), state.getAccelerationX(),
				state.getAccelerationY(), state.getAccelerationZ()
			);
		}

		public override string getMessageText()
		{
			return formatStringForMessage(
				x, y, z, pitch, roll, yaw,
				speedX, speedY, speedZ,
				lowTemperature, highTemperature,
				flightDistance, height,
				batteryPercentage, barometerMeasurement,
				motorTime,
				accelerationX, accelerationY, accelerationZ
			);
		}

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

			string s = stateFields[1].Split(':')[1];
			x = int.Parse(stateFields[1].Split(':')[1]);
			y = int.Parse(stateFields[2].Split(':')[1]);
			z = int.Parse(stateFields[3].Split(':')[1]);
			pitch = int.Parse(stateFields[5].Split(':')[1]);
			roll = int.Parse(stateFields[6].Split(':')[1]);
			yaw = int.Parse(stateFields[7].Split(':')[1]);
			speedX = int.Parse(stateFields[8].Split(':')[1]);
			speedY = int.Parse(stateFields[9].Split(':')[1]);
			speedZ = int.Parse(stateFields[10].Split(':')[1]);
			lowTemperature = int.Parse(stateFields[11].Split(':')[1]);
			highTemperature = int.Parse(stateFields[12].Split(':')[1]);
			flightDistance = int.Parse(stateFields[13].Split(':')[1]);
			height = int.Parse(stateFields[14].Split(':')[1]);
			batteryPercentage = int.Parse(stateFields[15].Split(':')[1]);
			barometerMeasurement = double.Parse(stateFields[16].Split(':')[1]);
			motorTime = int.Parse(stateFields[17].Split(':')[1]);
			accelerationX = double.Parse(stateFields[18].Split(':')[1]);
			accelerationY = double.Parse(stateFields[19].Split(':')[1]);
			accelerationZ = double.Parse(stateFields[20].Split(':')[1]);
		}
//
//		private int parseInteger(string expectedLabel, string data)
//		{
//			string valueToParse = getValueToParse(expectedLabel, data);
//			if (valueToParse == null)
//				return 0;
//
//			int result = 0;
//			try
//			{
//				result = int.Parse(valueToParse);
//			}
//			catch (Exception e)
//			{
//				Console.WriteLine("ERROR: Failed to parse to integer in Status class.");
//				Console.WriteLine(e.Message);
//			}
//
//			return result;
//		}
//
//		private double parseDouble(string expectedLabel, string data)
//		{
//			string valueToParse = getValueToParse(expectedLabel, data);
//			if (valueToParse == null)
//				return 0.0;
//
//			double result = 0.0;
//			try
//			{
//				result = double.Parse(valueToParse);
//			}
//			catch (Exception e)
//			{
//				Console.WriteLine("ERROR: Failed to parse to double in Status class.");
//				Console.WriteLine(e.Message);
//			}
//
//			return result;
//		}
//
//		private string getValueToParse(string expectedLabel, string data)
//		{
//			if (String.IsNullOrEmpty(expectedLabel) || String.IsNullOrEmpty(data))
//				return null;
//
//			string[] parts = data.Split(':');
//			if (parts.Length != 2)
//				return null;
//
//			if (parts[0].Trim() != expectedLabel)
//				return null;
//
//			return parts[1].Trim();
//		}
	}
}