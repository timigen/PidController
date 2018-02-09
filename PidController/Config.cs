using System;

namespace Pid
{
    public class Config
    {
        /// <summary>
        /// Setpoint
        /// </summary>
        public double Setpoint { get; }

        /// <summary>
        /// coefficient
        /// </summary>
        public double Kp { get; }

        /// <summary>
        /// coefficient
        /// </summary>
        public double Ki { get; }

        /// <summary>
        /// coefficient
        /// </summary>
        public double Kd { get; }

        /// <summary>
        /// Min
        /// </summary>
        public double Min { get; }

        /// <summary>
        /// Max
        /// </summary>
        public double Max { get; }

        /// <summary>
        /// configuration
        /// </summary>
        /// <param name="setpoint">setpoint</param>
        /// <param name="minOutput">minOutput</param>
        /// <param name="maxOutput">maxOutput</param>
        public Config(double setpoint, double minOutput, double maxOutput)
        {
            if (minOutput >= maxOutput) throw new Exception("invalid configuration!");

            Kp = 0.01;
            Ki = 0.005;
            Kd = 0.01;

            this.Setpoint = setpoint;
            this.Min = minOutput;
            this.Max = maxOutput;
        }
    }
}
