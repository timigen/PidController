namespace PidControllerTests
{
    public class ProcessLoop
    {
        public uint Id { get; set; }
        public double Setpoint { get; set; }
        public double ProcessValue { get; set; }
        public double Correction { get; set; }

        public ProcessLoop(uint id, double setpoint, double processValue, double correction)
        {
            Id = id;
            Setpoint = setpoint;
            ProcessValue = processValue;
            Correction = correction;
        }
    }
}
