using DiskIO.AvailableMetrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.SelectedMetrics
{
    interface ISelectedMetrics
    {
        Metric[] GetSelectedMetrics();
        void SetSelectedMetrics(Metric[] SelectedMetrics);
    }
}
