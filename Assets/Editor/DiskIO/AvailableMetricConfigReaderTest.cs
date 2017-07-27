using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using DiskIO.AvailableMetrics;
using System.Collections.Generic;

public class AvailableMetricConfigReaderTest
{

    [Test]
    public void ReadConfigFileTest()
    {
        List<Metric> avMetrics = new List<Metric>();
        avMetrics.Add(new Metric("Lines of Code", "ncloc", 0.0, "double"));
        avMetrics.Add(new Metric("Test Success Density", "test_success_density", 0.0, "percentage"));
        avMetrics.Add(new Metric("Code Smells", "code_smells", 0.0, "double"));
        avMetrics.Add(new Metric("Comment Lines Density", "comment_lines_density", 0.0, "percentage"));
        avMetrics.Add(new Metric("Bugs", "bugs", 0.0, "double"));
        avMetrics.Add(new Metric("Vulnerabilities", "vulnerabilities", 0.0, "double"));
        avMetrics.Add(new Metric("Violations", "violations", 0.0, "double"));
        avMetrics.Add(new Metric("Functions", "functions", 0.0, "double"));
        avMetrics.Add(new Metric("Coverage", "coverage", 0.0, "percentage"));
        
        List<Metric> readedMetrics = AvailableMetricConfigReader.ReadConfigFile();
        int i = 0;
        foreach (Metric m in readedMetrics)
        {
            //Debug.Log(m.name+ " "+ avMetrics.ToArray()[i].name);
            //Debug.Log(m.key + " " + avMetrics.ToArray()[i].key);
            //Debug.Log(m.defaultValue + " " + avMetrics.ToArray()[i].defaultValue);
            //Debug.Log(m.datatype + " " + avMetrics.ToArray()[i].datatype);
            Assert.AreEqual(m, avMetrics.ToArray()[i]);
            i++;
        }
    }
}
