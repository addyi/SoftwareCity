using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiskIO.AvailableMetrics;

namespace DataModel.Metrics
{
    interface IAvailableMetrics
    {
        List<Metric> GetAvailableMetrics();
        void SetAvailableMetrics(List<Metric> AvailableMetrics);
        string GetAvailableMetricsAsString();
    }
}
