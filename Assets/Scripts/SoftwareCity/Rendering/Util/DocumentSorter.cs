using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SoftwareCity.Rendering.Utils
{
    public class DocumentSorter : MonoBehaviour
    {
        /// <summary>
        /// Sorting documents by height in descending order.
        /// </summary>
        /// <param name="childs"></param>
        /// <returns></returns>
        public static List<GameObject> SortingByHeightDesc(List<GameObject> childs)
        {
            return childs.OrderByDescending(child => child.GetComponentInChildren<Renderer>().bounds.size.y).ToList();
        }

        /// <summary>
        /// Sorting documents by height in ascending order.
        /// </summary>
        /// <param name="childs"></param>
        /// <returns></returns>
        public static List<GameObject> SortingByHeightAsc(List<GameObject> childs)
        {
            return childs.OrderBy(child => child.GetComponentInChildren<Renderer>().bounds.size.y).ToList();
        }

        /// <summary>
        /// Sorting documents by width in descending order.
        /// </summary>
        /// <param name="childs"></param>
        /// <returns></returns>
        public static List<GameObject> SortingByWidthDesc(List<GameObject> childs)
        {
            return childs.OrderByDescending(child => child.GetComponentInChildren<Renderer>().bounds.size.x).ToList();
        }

        /// <summary>
        /// Sorting documents by width in ascending order.
        /// </summary>
        /// <param name="childs"></param>
        /// <returns></returns>
        public static List<GameObject> SortingByWidthAsc(List<GameObject> childs)
        {
            return childs.OrderBy(child => child.GetComponentInChildren<Renderer>().bounds.size.x).ToList();
        }

        /// <summary>
        /// Sorting documents by height and width in descending order.
        /// </summary>
        /// <param name="childs"></param>
        /// <returns></returns>
        public static List<GameObject> SortingByHeightAndWidthDesc(List<GameObject> childs)
        {
            return childs.OrderByDescending(child => child.GetComponentInChildren<Renderer>().bounds.size.x + child.GetComponentInChildren<Renderer>().bounds.size.y).ToList();
        }

        /// <summary>
        /// Sorting docments by height and width in ascending order.
        /// </summary>
        /// <param name="childs"></param>
        /// <returns></returns>
        public static List<GameObject> SortingByHeightAndWidthAsc(List<GameObject> childs)
        {
            return childs.OrderBy(child => child.GetComponentInChildren<Renderer>().bounds.size.x + child.GetComponentInChildren<Renderer>().bounds.size.y).ToList();
        }
    }
}
