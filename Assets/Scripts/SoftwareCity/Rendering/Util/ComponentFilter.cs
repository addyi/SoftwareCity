using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoftwareCity.Rendering.Utils
{
    public class ComponentFilter : MonoBehaviour
    {

        /// <summary>
        /// Get all documents from childs.
        /// </summary>
        /// <param name="childs"></param>
        /// <returns></returns>
        public static List<GameObject> FilterDocuments(List<GameObject> childs)
        {
            List<GameObject> documents = new List<GameObject>();
            foreach (GameObject child in childs)
            {
                if (child.GetComponent<Information>().GetSQObjectType().Equals("document"))
                    documents.Add(child);
            }

            return documents;
        }

        /// <summary>
        /// Get all packages from childs.
        /// </summary>
        /// <param name="childs"></param>
        /// <returns></returns>
        public static List<GameObject> FilterPackages(List<GameObject> childs)
        {
            List<GameObject> packages = new List<GameObject>();
            foreach (GameObject child in childs)
            {
                if (child.GetComponent<Information>().GetSQObjectType().Equals("package"))
                    packages.Add(child);
            }

            return packages;
        }
    }
}
