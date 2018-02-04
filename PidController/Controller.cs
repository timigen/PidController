using PidController;

namespace Pid
{
    public class Controller
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

        public Controller(Config config)
        {
            P = I = D = 0;
            Kp = config.Kp;
            Ki = config.Ki;
            Kd = config.Kd;
            Setpoint = config.Setpoint;
        }

        public double SetProcessValue(double value, long dX)
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

            return (P * Kp) + (I * Ki) + (D * Kd);
        }
    }
}
