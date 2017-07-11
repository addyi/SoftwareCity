using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DiskIO.AvailableMetrics
{
    public static class AvailableMetricConfigReader
    {
        public static readonly string ConfigFilePath = Application.dataPath + "/Scripts/DiskIO/AvailableMetrics/availablemetricsConf.json";

        public static List<Metric> ReadConfigFile()
        {
            StreamReader r = new StreamReader(ConfigFilePath);
            string json = r.ReadToEnd();
            AvailableMetrics av = JsonUtility.FromJson<AvailableMetrics>(json);
            return av.availablemetrics;
        }
    }
}
