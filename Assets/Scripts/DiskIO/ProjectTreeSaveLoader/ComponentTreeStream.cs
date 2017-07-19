using UnityEngine;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;

namespace DiskIO.ProjectTreeSaveLoader
{

    public static class ComponentTreeStream
    {
        private static readonly string path = Application.dataPath + "/Storage/localProjectTreeStore.data";

        public static void SaveProjectComponent<T>(T obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (XmlDictionaryWriter writer = XmlDictionaryWriter.CreateBinaryWriter(ms))
                {
                    DataContractSerializer dcs = new DataContractSerializer(typeof(T));

                    dcs.WriteObject(writer, obj);
                    writer.Flush();
                    File.WriteAllBytes(path, ms.ToArray());
                }
            }
        }

        public static T LoadProjectComponent<T>()
        {
            byte[] data = File.ReadAllBytes(path);
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                using (XmlDictionaryReader reader = XmlDictionaryReader.CreateBinaryReader(memoryStream, XmlDictionaryReaderQuotas.Max))
                {
                    DataContractSerializer dcs = new DataContractSerializer(typeof(T));
                    return (T)dcs.ReadObject(reader);
                }
            }

        }

    }
}


