using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;
//using UnityEngine.Windows;

namespace DiskIO.AvailableMetrics
{
    /// <summary>
    /// Static class for reading predefined metrics from JSON file.
    /// These metrics are requested by the WebInterface as selectable to the user.
    /// </summary>
    public static class AvailableMetricConfigReader
    {
        /// <summary>
        /// Path to JSON file with availabe metrics.
        /// </summary>
        private static readonly string ConfigFilePath = Application.dataPath + "/Storage/availablemetricsConf.json";

        /// <summary>
        /// Reading predefined metrics that are requested by the WebInterface to SonarQube.
        /// These metrics are selectable to the user to show the City.
        /// </summary>
        /// <returns>List of predefined metrics</returns>
        public static List<Metric> ReadConfigFile()
        {
            byte[] data = File.ReadAllBytes(ConfigFilePath);
            string json = Encoding.UTF8.GetString(data, 0, data.Count());
            AvailableMetrics av = JsonUtility.FromJson<AvailableMetrics>(json);
            return av.availablemetrics;
        }
    }
}
