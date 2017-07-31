using NUnit.Framework;
using DiskIO.AvailableMetrics;
using System.Collections.Generic;

public class AvailableMetricConfigReaderTest
{
    [Test]
    public void ConfigFileIsReadableAndHasMin4Metrics()
    {
        List<Metric> readedMetrics = AvailableMetricConfigReader.ReadConfigFile();
        Assert.GreaterOrEqual(readedMetrics.Count, 4);
    }
}
