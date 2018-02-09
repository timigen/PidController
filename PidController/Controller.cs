namespace Pid
{
    public class Controller : IController
    {
        private double P;
        private double I;
        private double D;

        private double Offset;
        private double PreviousOffset;

        private Config Config;

        public Controller(Config config)
        {
            P = I = D = 0;
            Config = config;
        }

        public double GetCorrection(double value, long dX)
        {
            Offset = Config.Setpoint - value;

            if (Offset == 0)
            {
                return 0;
            }

            P = Offset;
            I += Offset * dX;
            D = (Offset - PreviousOffset) / dX;

            PreviousOffset = Offset;

            double output = LimitOutput((P * Config.Kp) + (I * Config.Ki) + (D * Config.Kd));

            if (Offset < 0 && output > 0) { return output * -1; }

            return output;
        }

        private double LimitOutput(double output)
        {
            if (Offset < Config.Max && Offset > Config.Min)
            {
                return Offset;
            }

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
