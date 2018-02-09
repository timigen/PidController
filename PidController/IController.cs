namespace Pid
{
    /// <summary>
    /// IController
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// get correction
        /// </summary>
        /// <param name="processValue">processValue</param>
        /// <param name="dX">dX</param>
        /// <returns>correction value</returns>
        double GetCorrection(double processValue, long dX);
    }
}
