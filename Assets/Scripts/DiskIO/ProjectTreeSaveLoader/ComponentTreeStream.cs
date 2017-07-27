using UnityEngine;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;
using DataModel.ProjectTree.Components;

namespace DiskIO.ProjectTreeSaveLoader
{
    /// <summary>
    /// Static class for saving and loading a ProjectComponent object. 
    /// </summary>
    public static class ComponentTreeStream
    {
        //private static readonly string path = Application.dataPath + "/Storage/localProjectTreeStore.data";

        /// <summary>
        /// Path to saving the ProjectComponent object
        /// </summary>
        private static readonly string path = Path.Combine(Application.streamingAssetsPath, "localProjectTreeStore.data");

        /// <summary>
        /// serialize and save ProjectComponent
        /// </summary>
        /// <param name="projectComponent"></param>
        public static void SaveProjectComponent(ProjectComponent projectComponent)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (XmlDictionaryWriter writer = XmlDictionaryWriter.CreateBinaryWriter(ms))
                {
                    DataContractSerializer dcs = new DataContractSerializer(typeof(ProjectComponent));

                    dcs.WriteObject(writer, projectComponent);
                    writer.Flush();
                    File.WriteAllBytes(path, ms.ToArray());
                }
            }
        }

        /// <summary>
        /// load and deserialize the ProjectComponent object
        /// </summary>
        /// <returns></returns>
        public static ProjectComponent LoadProjectComponent()
        {

            if(File.Exists(path))
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
            return null;
        }

    }
}


