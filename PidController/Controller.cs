using PidController;

namespace Pid
{
    public class Controller : IController
    {
        private double P;
        private double I;
        private double D;

        private double Difference;
        private double PreviousDifference;

        private Config Config;

        public Controller(Config config)
        {
            P = I = D = 0;
            Config = config;
        }

        public double GetCorrection(double value, long dX)
        {
            Difference = Config.Setpoint - value;

            if (Difference == 0)
            {
                return 0;
            }

            P = Difference;
            I += Difference * dX;
            D = (Difference - PreviousDifference) / dX;

            PreviousDifference = Difference;

            return LimitOutput((P * Config.Kp) + (I * Config.Ki) + (D * Config.Kd));
        }

        private double LimitOutput(double output)
        {
            if (output < Config.Min)
            {
                return Config.Min;
            }

            if (output > Config.Max)
            {
                return Config.Max;
            }

            return output;
        }
    }
}
