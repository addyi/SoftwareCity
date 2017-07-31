using UnityEngine;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;
using DataModel.ProjectTree.Components;

#if !UNITY_EDITOR && UNITY_METRO
using Windows.Storage;
#endif

namespace DiskIO.ProjectTreeSaveLoader
{
    /// <summary>
    /// Static class for saving and loading a ProjectComponent object. 
    /// </summary>
    public static class ComponentTreeStream
    {
        /// <summary>
        /// Path to saving the ProjectComponent object
        /// </summary>

        //private static readonly string path = Application.dataPath + "/Storage/localProjectTreeStore.data";
#if !UNITY_EDITOR && UNITY_METRO
        private static readonly string path = Path.Combine(ApplicationData.Current.RoamingFolder.Path, "localProjectTreeStore.data");
#else
        private static readonly string path = Path.Combine(Application.persistentDataPath, "localProjectTreeStore.data");
#endif

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
#if !UNITY_EDITOR && UNITY_METRO
                    Debug.Log("Windows--->SaveProjectComponent: " + path);
#else
                    Debug.Log("Desktop--->SaveProjectComponent: " + path);
#endif
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
#if !UNITY_EDITOR && UNITY_METRO
                    Debug.Log("Windows--->LoadProjectComponent: " + path);
#else
                Debug.Log("Desktop--->LoadProjectComponent: " + path);
#endif
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


