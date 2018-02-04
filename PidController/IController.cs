namespace PidController
{
    public interface IController
    {
        double GetCorrection(double processValue, long dX);
    }
}
