using DiskIO.AvailableMetrics;

namespace DataModel.Metrics
{
    /// <summary>
    /// Methods the Model needs to provide for SelectedMetric handling
    /// </summary>
    interface ISelectedMetrics
    {
        /// <summary>
        /// Returns the selected metrics array
        /// </summary>
        /// <returns>Selected metrics array</returns>
        Metric[] GetSelectedMetrics();
        /// <summary>
        /// Sets the metrics the user has selected
        /// </summary>
        /// <param name="SelectedMetrics">Metrics the user has selected</param>
        void SetSelectedMetrics(Metric[] SelectedMetrics);
    }
}
