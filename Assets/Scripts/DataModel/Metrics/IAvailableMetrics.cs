using System.Collections.Generic;
using DiskIO.AvailableMetrics;

namespace DataModel.Metrics
{
    /// <summary>
    /// Methods the Model needs to provide for AvailableMetrics handling
    /// </summary>
    interface IAvailableMetrics
    {
        /// <summary>
        /// Returns the list of available metrics
        /// </summary>
        /// <returns>List of available metrics</returns>
        List<Metric> GetAvailableMetrics();
        /// <summary>
        /// Returns a dictionary of available metrics with the metric key as key
        /// </summary>
        /// <returns>Dictionary of available metrics</returns>
        Dictionary<string, Metric> GetAvailableMetricsAsDictionary();
        /// <summary>
        /// Sets the AvailableMetrics list e.g. if the 
        /// list is read from the config file
        /// </summary>
        /// <param name="AvailableMetrics">AvailableMetrics List</param>
        void SetAvailableMetrics(List<Metric> AvailableMetrics);
        /// <summary>
        /// Returns the list of available metrics as a single string.
        /// This is used for the API request.
        /// </summary>
        /// <returns>List of available metrics as a single string</returns>
        string GetAvailableMetricsAsString();
    }
}
