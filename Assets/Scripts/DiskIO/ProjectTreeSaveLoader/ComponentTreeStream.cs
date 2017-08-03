using UnityEngine;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;
using DataModel.ProjectTree.Components;
using System.Text;

namespace DiskIO.ProjectTreeSaveLoader
{
    /// <summary>
    /// Static class for saving and loading a ProjectComponent object. 
    /// </summary>
    public static class ComponentTreeStream
    {
        private static readonly string projectKey = "project";

        /// <summary>
        /// serialize and save ProjectComponent
        /// </summary>
        /// <param name="projectComponent"></param>
        public static void SaveProjectComponent(ProjectComponent projectComponent)
        {
            string json = JsonUtility.ToJson(projectComponent);
            PlayerPrefs.SetString(projectKey, json);
            Debug.Log("<--- Save PlayerPrefs");
        }

        /// <summary>
        /// load and deserialize the ProjectComponent object
        /// </summary>
        /// <returns></returns>
        public static ProjectComponent LoadProjectComponent()
        {
            if (PlayerPrefs.HasKey(projectKey))
            {
                string json = PlayerPrefs.GetString(projectKey);
                ProjectComponent resObj = JsonUtility.FromJson<ProjectComponent>(json);
                Debug.Log("---> Load PlayerPrefs" + resObj.ToString());
                return resObj;
            }
            return null;
        }

    }
}


