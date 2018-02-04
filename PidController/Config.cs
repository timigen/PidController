﻿using System;

namespace Pid
{
    public class Config
    {
        public double Setpoint { get; }

        public double Kp { get; }
        public double Ki { get; }
        public double Kd { get; }

        public double Min { get; }
        public  double Max { get; }

        public Config(double setpoint, double minOutput, double maxOutput)
        {
            if (minOutput >= maxOutput) throw new Exception("invalid configuration!");

            Kp = 0.001;
            Ki = 0.00005;
            Kd = 0.0001;
            Setpoint = setpoint;
            Min = minOutput;
            Max = maxOutput;
        }
    }
}
