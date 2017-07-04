using System.IO;

namespace QAware.DataStorage.DiskIOManager
{
    public class DiskIOStream<Type>
    {
        public DiskIOStream(){
        }

        public void SerializeDiskIOItem(string file, Type item)
        {
            //serialize
            using (Stream stream = File.Open(file, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bformatter.Serialize(stream, item);
            }
        }

        public Type DeserializeDiskIOItem(string file)
        {
            //deserialize
            using (Stream stream = File.Open(file, FileMode.Open))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (Type)bformatter.Deserialize(stream);

            }
           
        }
    }
}