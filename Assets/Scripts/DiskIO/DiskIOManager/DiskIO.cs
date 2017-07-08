using System.IO;

namespace DiskIO.DiskIOManager
{
    public class DiskIO<Type>
    {
        public readonly string directoryPath = @"/hololens/directory/path/where/componentTree/should-be-saved ";
        public readonly string filePath = @"/file/path/where/componentTree/should-be-saved";

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
