using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using DiskIO.AvailableMetrics;
using System.Collections.Generic;

public class DiskIOTest
{

    [Test]
    public void ReadConfigFileTest()
    {
        List<Metric> avMetrics = new List<Metric>();
        avMetrics.Add(new Metric("Lines of Code", "ncloc", 0.1f, "int"));
        avMetrics.Add(new Metric("test succes", "ts", 100.0f, "percentege"));


        List<Metric> readedMetrics = AvailableMetricConfigReader.ReadConfigFile();
        int i = 0;
        foreach (Metric m in readedMetrics)
        {
            Assert.AreEqual(m, avMetrics.ToArray()[i]);
            i++;
        }



    }


}
