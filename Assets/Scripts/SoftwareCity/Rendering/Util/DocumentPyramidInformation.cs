using UnityEngine;

namespace SoftwareCity.Rendering.Utils
{
    public class DocumentPyramidInformation : MonoBehaviour {

        /// <summary>
        /// Pyramid corner at the top in the north east direction.
        /// </summary>
        [SerializeField]
        private GameObject topNorthEast;

        /// <summary>
        /// Pyramid corner at the top in the north west direction.
        /// </summary>
        [SerializeField]
        private GameObject topNorthWest;

        /// <summary>
        /// Pyramid corner at the top in the south east direction.
        /// </summary>
        [SerializeField]
        private GameObject topSouthEast;

        /// <summary>
        /// Pyramid corner at the top in the south west direction.
        /// </summary>
        [SerializeField]
        private GameObject topSouthWest;

        /// <summary>
        /// Set the correct position of the pyramid corners.
        /// </summary>
        /// <param name="percent"></param>
        public void SetPosition(float percent)
        {
            topNorthEast.transform.localPosition = new Vector3(topNorthEast.transform.localPosition.x * percent, topNorthEast.transform.localPosition.y, topNorthEast.transform.localPosition.z * percent);
            topNorthWest.transform.localPosition = new Vector3(topNorthWest.transform.localPosition.x * percent, topNorthWest.transform.localPosition.y, topNorthWest.transform.localPosition.z * percent);
            topSouthEast.transform.localPosition = new Vector3(topSouthEast.transform.localPosition.x * percent, topSouthEast.transform.localPosition.y, topSouthEast.transform.localPosition.z * percent);
            topSouthWest.transform.localPosition = new Vector3(topSouthWest.transform.localPosition.x * percent, topSouthWest.transform.localPosition.y, topSouthWest.transform.localPosition.z * percent);
        }
    }
}

