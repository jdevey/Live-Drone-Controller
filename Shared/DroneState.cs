using System;

namespace Shared
{
	public class DroneState
	{
		private bool inCommandMode;
    private bool takenOff;
    private bool videoStreamOn;
    private DateTime stateTimestamp;
    private double currentFlightTime;
    private double positionX;
    private double positionY;
    private double positionZ;
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
    private int orientation;

    public DroneState() {
        resetState();
    }

    public bool isInCommandMode() { return inCommandMode; }

    public void setInCommandMode(bool inCommandMode) {
        if (this.inCommandMode == inCommandMode)
            return;

        this.inCommandMode = inCommandMode;
        if (!inCommandMode)
            resetState();
    }

    public bool hasTakenOff() {
        return takenOff;
    }

    public void setHasTakenOff(bool takenOff) {
        if (this.takenOff == takenOff)
            return;

        this.takenOff = inCommandMode && takenOff;
        if (!this.takenOff)
            resetFlyingInfo();
    }

    public bool isVideoStreamOn() { return videoStreamOn; }

    public void setVideoStreamOn(bool videoStreamOn)
    {
        this.videoStreamOn = inCommandMode && videoStreamOn;
    }

    public double getCurrentFlightTime() {
        return currentFlightTime;
    }

    public void setCurrentFlightTime(double currentFlightTime) {
        if (inCommandMode) {
            this.currentFlightTime = currentFlightTime;
        }
    }

    public void updateFlyingInfo(Status status) {
        if (!inCommandMode || status==null) {
            return;
        }

        pitch = status.getPitch();
        roll = status.getRoll();
        yaw = status.getYaw();
        speedX = status.getSpeedX();
        speedY = status.getSpeedY();
        speedZ = status.getSpeedZ();
        lowTemperature = status.getLowTemperature();
        highTemperature = status.getHighTemperature();
        flightDistance = status.getFlightDistance();
        height = status.getHeight();
        batteryPercentage = status.getBatteryPercentage();
        barometerMeasurement = status.getBarometerMeasurement();
        motorTime = status.getMotorTime();
        accelerationX = status.getAccelerationX();
        accelerationY = status.getAccelerationY();
        accelerationZ = status.getAccelerationZ();

        stateTimestamp = new DateTime();
    }

    public DateTime getStateTimestamp() {
        return stateTimestamp;
    }
    
    private static double toRadians(double angle)
    {
        return (Math.PI / 180) * angle;
    }

    public void move(double deltaX, double deltaY, double deltaZ) {
        if (!takenOff) return;

        double rotation = toRadians(-orientation);
        double rotatedX = Math.Cos(rotation) * deltaX - Math.Sin(rotation) * deltaY;
        double rotatedY = Math.Sin(rotation) * deltaX + Math.Cos(rotation) * deltaY;

        positionX += rotatedX;
        positionY += rotatedY;
        positionZ += deltaZ;
    }

    public void rotate(int deltaOrientation) {
        if (!takenOff) return;

        orientation += deltaOrientation;
        orientation = orientation % 360;
    }

    public double getPositionX() {
        return positionX;
    }

    public double getPositionY() {
        return positionY;
    }

    public double getPositionZ() {
        return positionZ;
    }

    public int getPitch() {
        return pitch;
    }

    public int getRoll() {
        return roll;
    }

    public int getYaw() {
        return yaw;
    }

    public int getSpeedX() {
        return speedX;
    }

    public int getSpeedY() {
        return speedY;
    }

    public int getSpeedZ() {
        return speedZ;
    }

    public double getAccelerationX() {
        return accelerationX;
    }

    public double getAccelerationY() {
        return accelerationY;
    }

    public double getAccelerationZ() {
        return accelerationZ;
    }

    public int getLowTemperature() {
        return lowTemperature;
    }

    public int getHighTemperature() {
        return highTemperature;
    }

    public int getFlightDistance() {
        return flightDistance;
    }

    public int getHeight() {
        return height;
    }

    public int getBatteryPercentage() {
        return batteryPercentage;
    }

    public double getBarometerMeasurement() {
        return barometerMeasurement;
    }

    public int getMotorTime() {
        return motorTime;
    }

    public int getOrientation() { return orientation; }

    private void resetState() {
        videoStreamOn = false;
        takenOff = false;
        stateTimestamp = new DateTime();
        resetFlyingInfo();
    }

    private void resetFlyingInfo() {
        currentFlightTime = 0.0;
        positionX = 0.0;
        positionY = 0.0;
        positionZ = 0.0;
        pitch = 0;
        roll = 0;
        yaw = 0;
        speedX = 0;
        speedY = 0;
        speedZ = 0;
        lowTemperature = 0;
        highTemperature = 0;
        flightDistance = 0;
        height = 0;
        batteryPercentage = 0;
        barometerMeasurement = 0.0;
        motorTime = 0;
        accelerationX = 0.0;
        accelerationY = 0.0;
        accelerationZ = 0.0;
        orientation = 0;
    }
	}
}