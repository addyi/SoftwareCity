using System.Collections.Generic;
using System.IO;
using QAware.DataStorage.DataEntertainment;
using DiskIO.ReadConfigFile;

namespace DiskIO.DiskIOManager
{
    public class DiskIO
    {
        private readonly string directoryPath = @"/Users/yacinesghairi/Desktop";

		public DiskIO()
		{
		}

        //public ConfigFile ConfigFile { get; set; }
        //public ComponentTree ComponentTree { get; set; }
        //public SQCredential Credentials { get; set; }

        public List<Metric> GetAvailableMetrics(ConfigFile configFile)
        {
            return configFile.ReadConfigFile("/Users/yacinesghairi/Desktop/MyStacks/QAware-statics/av-metrics/available.json");
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
