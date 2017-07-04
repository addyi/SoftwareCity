
namespace DiskIO.ReadConfigFile
{
    public class Availablemetrics
    {
        /// <summary>
        /// The availablemetrics.
        /// </summary>
        public Metric[] availablemetrics;

        /// <summary>
        /// Gets the availablemetrics.
        /// </summary>
        /// <returns>Metrics[]</returns>
        public Metric[] GetAvailablemetrics()
        {
            return availablemetrics;
        }

        /// <summary>
        /// Sets the availablemetrics.
        /// </summary>
        /// <param name="metrics">Metrics[]</param>
        public void SetAvailablemetrics(Metric[] metrics)
        {
            availablemetrics = metrics;
        }
    }
}
