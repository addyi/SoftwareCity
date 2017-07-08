using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
//using Newtonsoft.Json;

namespace DiskIO.ReadConfigFile
{
    public class ConfigFile
    {
        public List<Metric> ReadConfigFile(string filePath)
        {
            StreamReader r = new StreamReader(filePath);
            string json = r.ReadToEnd();
            Availablemetrics av = new Availablemetrics();
            //av = JsonConvert.DeserializeObject<Availablemetrics>(json);
            av = JsonUtility.FromJson<Availablemetrics>(json);
            List<Metric> availablemetrics = new List<Metric>();

            foreach (Metric m in av.availablemetrics)
                availablemetrics.Add(m);
            return availablemetrics;
        }

        public void WriteConfigFile(string filePath, List<Metric> availablemetrics)
        {
            Availablemetrics av = new Availablemetrics();
            av.availablemetrics = availablemetrics.ToArray();
            //string json = JsonConvert.SerializeObject(av);
            string json = JsonUtility.ToJson(av);
            File.WriteAllText(@filePath, json);
        }
    }
}
