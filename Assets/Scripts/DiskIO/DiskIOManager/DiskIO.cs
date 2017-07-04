using System;
using System.Collections.Generic;
using System.IO;
using QAware.DataStorage.DataEntertainment;
using QAware.DataStorage.ReadConfigFile;

namespace QAware.DataStorage.DiskIOManager
{
    [Serializable]
    public class DiskIO
    {
        private ConfigFile configFile;
        private ComponentTree componentTree;
        private SQCredential credentials;
        private readonly string directoryPath = @"/Users/yacinesghairi/Desktop";

        public DiskIO(){
        }

        public ConfigFile ConfigFile { get => configFile; set => configFile = value; }
        public ComponentTree ComponentTree { get => componentTree; set => componentTree = value; }
        public SQCredential Credentials { get => credentials; set => credentials = value; }

        public List<Metric> GetAvailableMetrics(ConfigFile configFile){
            return configFile.ReadKonfigFile("/Users/yacinesghairi/Desktop/MyStacks/QAware-statics/av-metrics/available.json");
        }

        public ComponentTree GetSavedTree(string fileName)
        {	
			string file = Path.Combine(directoryPath, fileName);
            DiskIOStream<ComponentTree> dskioStream = new DiskIOStream<ComponentTree>();
            return dskioStream.DeserializeDiskIOItem(file);

        }

		public void SetSavedTree(string fileName, ComponentTree componentTree)
		{
			string file = Path.Combine(directoryPath, fileName);
            DiskIOStream<ComponentTree> dskioStream = new DiskIOStream<ComponentTree>();
            dskioStream.SerializeDiskIOItem(file, componentTree);
		}
    }
}
