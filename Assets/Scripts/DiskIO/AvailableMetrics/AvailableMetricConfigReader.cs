using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Windows;

namespace DiskIO.AvailableMetrics
{
    public static class AvailableMetricConfigReader
    {
        public static readonly string ConfigFilePath = Application.dataPath + "/Scripts/DiskIO/AvailableMetrics/availablemetricsConf.json";

        public static List<Metric> ReadConfigFile()
        {
            byte[] data = File.ReadAllBytes(ConfigFilePath);
            string json = Encoding.UTF8.GetString(data, 0, data.Count());
            AvailableMetrics av = JsonUtility.FromJson<AvailableMetrics>(json);
            return av.availablemetrics;
        }
    }
}
