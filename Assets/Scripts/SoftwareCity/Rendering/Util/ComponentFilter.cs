using DataModel;
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
                if (child.GetComponent<BaseInformation>().GetQualifier() == SqQualifier.FILE 
                    || child.GetComponent<BaseInformation>().GetQualifier() == SqQualifier.UNIT_TEST)
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
                if (child.GetComponent<BaseInformation>().GetQualifier() == SqQualifier.DIRECTORY 
                    || child.GetComponent<BaseInformation>().GetQualifier() == SqQualifier.PROJECT 
                    || child.GetComponent<BaseInformation>().GetQualifier() == SqQualifier.SUB_PROJECT)
                    packages.Add(child);
            }

            return packages;
        }
    }
}
