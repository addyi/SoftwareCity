using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SoftwareCity.Rendering.Utils
{
    public class DocumentSorter : MonoBehaviour
    {

        public static List<GameObject> SortingByHeightDesc(List<GameObject> childs)
        {
            return childs.OrderByDescending(child => child.GetComponentInChildren<Renderer>().bounds.size.y).ToList();
        }

        public static List<GameObject> SortingByHeightAsc(List<GameObject> childs)
        {
            return childs.OrderBy(child => child.GetComponentInChildren<Renderer>().bounds.size.y).ToList();
        }

        public static List<GameObject> SortingByWidthDesc(List<GameObject> childs)
        {
            return childs.OrderByDescending(child => child.GetComponentInChildren<Renderer>().bounds.size.x).ToList();
        }

        public static List<GameObject> SortingByWidthAsc(List<GameObject> childs)
        {
            return childs.OrderBy(child => child.GetComponentInChildren<Renderer>().bounds.size.x).ToList();
        }

        public static List<GameObject> SortingByHeightAndWidthDesc(List<GameObject> childs)
        {
            return childs.OrderByDescending(child => child.GetComponentInChildren<Renderer>().bounds.size.x + child.GetComponentInChildren<Renderer>().bounds.size.y).ToList();
        }

        public static List<GameObject> SortingByHeightAndWidthAsc(List<GameObject> childs)
        {
            return childs.OrderBy(child => child.GetComponentInChildren<Renderer>().bounds.size.x + child.GetComponentInChildren<Renderer>().bounds.size.y).ToList();
        }
    }
}
