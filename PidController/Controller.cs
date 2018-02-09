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
            this.P = this.I = this.D = 0;
            Config = config;
        }

        public double GetCorrection(double value, long dX)
        {
            Offset = this.Config.Setpoint - value;

            if (Offset == 0)
            {
                return 0;
            }

            this.P = Offset;
            this.I += Offset * dX;
            this.D = (Offset - PreviousOffset) / dX;

            PreviousOffset = Offset;

            double output = LimitOutput((this.P * this.Config.Kp) + (this.I * this.Config.Ki) + (this.D * this.Config.Kd));

            if (Offset < 0 && output > 0) { return output * -1; }

            return output;
        }

        private double LimitOutput(double output)
        {
            var min = Config.Min;
            var max = Config.Max;

            if (Offset < max && Offset > Config.Min)
            {
                return Offset;
            }

            if (output < min)
            {
                return min;
            }

            if (output > max)
            {
                return max;
            }

            return output;
        }
    }
}
