using System.Collections.Generic;
using System.IO;
using DiskIO.ReadConfigFile;

namespace DiskIO.DiskIOManager
{
    public class DiskIO<Type>
    {
        private readonly string directoryPath = @"/Users/yacinesghairi/Desktop";
		private readonly string filePath = @"/Users/yacinesghairi/Desktop/MyStacks/QAware-statics/av-metrics/available.json";

		public DiskIO()
		{
		}

        //public ConfigFile ConfigFile { get; set; }
        //public ComponentTree ComponentTree { get; set; }
        //public SQCredential Credentials { get; set; }

        public List<Metric> GetAvailableMetrics(ConfigFile configFile)
        {
            return configFile.ReadConfigFile(filePath);
        }

        public Type GetSavedTree(string fileName)
        {
            string file = Path.Combine(directoryPath, fileName);
            DiskIOStream<Type> dskioStream = new DiskIOStream<Type>();
            return dskioStream.DeserializeDiskIOItem(file);

        }

        public void SetSavedTree(string fileName, Type componentTree)
        {
            string file = Path.Combine(directoryPath, fileName);
            DiskIOStream<Type> dskioStream = new DiskIOStream<Type>();
            dskioStream.SerializeDiskIOItem(file, componentTree);
        }
    }
}
