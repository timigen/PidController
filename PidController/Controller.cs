using PidController;

namespace Pid
{
    public class Controller : IController
    {
        private double P;
        private double I;
        private double D;

        private double Kp;
        private double Ki;
        private double Kd;

        private double Difference;
        private double PreviousDifference;
        private double Setpoint;

        private double OutputMin;
        private double OutputMax;

        public Controller(Config config)
        {
            P = I = D = 0;
            Kp = config.Kp;
            Ki = config.Ki;
            Kd = config.Kd;
            Setpoint = config.Setpoint;

            OutputMin = config.Min;
            OutputMax = config.Max;
        }

        public double GetCorrection(double value, long dX)
        {
            Difference = Setpoint - value;

            if (Difference == 0)
            {
                return 0;
            }

            P = Difference;
            I += Difference * dX;
            D = (Difference - PreviousDifference) / dX;

            PreviousDifference = Difference;

            return LimitOutput((P * Kp) + (I * Ki) + (D * Kd));
        }

        private double LimitOutput(double rawValue)
        {
            if (rawValue < OutputMin)
            {
                return OutputMin;
            }

            if (rawValue > OutputMax)
            {
                return OutputMax;
            }

            return rawValue;
        }
    }
}
