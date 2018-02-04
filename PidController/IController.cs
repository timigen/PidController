namespace Pid
{
    public interface IController
    {
        double GetCorrection(double processValue, long dX);
    }
}
