﻿using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace QAware.DataStorage.ReadConfigFile
{
    public class ConfigFile
    {
		public  List<Metric> ReadKonfigFile(string filePath)
		{
            StreamReader r = new StreamReader(filePath);
			string json = r.ReadToEnd();
            Availablemetrics av = new Availablemetrics();
			av = JsonConvert.DeserializeObject<Availablemetrics>(json);
			List<Metric> availablemetrics = new List<Metric>();

			foreach (Metric m in av.availablemetrics)
			    availablemetrics.Add(m);
            return availablemetrics;	
		}

        public void WriteKonfigFile(string filePath, List<Metric> availablemetrics)
        {
            Availablemetrics av = new Availablemetrics();
            av.availablemetrics = availablemetrics.ToArray();
            string json = JsonConvert.SerializeObject(av);
            File.WriteAllText(@filePath, json);
        }
    }
}
