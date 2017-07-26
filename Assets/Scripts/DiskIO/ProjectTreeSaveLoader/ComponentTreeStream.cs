using UnityEngine;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;
using DataModel.ProjectTree.Components;

namespace DiskIO.ProjectTreeSaveLoader
{

    public static class ComponentTreeStream
    {
        //private static readonly string path = Application.dataPath + "/Storage/localProjectTreeStore.data";

        private static readonly string path = Path.Combine(Application.streamingAssetsPath, "localProjectTreeStore.data");

        public static void SaveProjectComponent(ProjectComponent obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (XmlDictionaryWriter writer = XmlDictionaryWriter.CreateBinaryWriter(ms))
                {
                    DataContractSerializer dcs = new DataContractSerializer(typeof(ProjectComponent));

                    dcs.WriteObject(writer, obj);
                    writer.Flush();
                    File.WriteAllBytes(path, ms.ToArray());
                }
            }
        }

        public static ProjectComponent LoadProjectComponent()
        {

            byte[] data = UnityEngine.Windows.File.ReadAllBytes(path);
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                using (XmlDictionaryReader reader = XmlDictionaryReader.CreateBinaryReader(memoryStream, XmlDictionaryReaderQuotas.Max))
                {
                    DataContractSerializer dcs = new DataContractSerializer(typeof(ProjectComponent));
                    return (ProjectComponent)dcs.ReadObject(reader);
                }
            }

        }

    }
}


