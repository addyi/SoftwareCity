using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoftwareCity.Infobox
{
    public class InfoboxHandler : MonoBehaviour
    {
        /// <summary>
        /// With of the current infobox.
        /// </summary>
        private float infoboxWidth = -1.0f;
        
        /// <summary>
        /// Update the infobox position.
        /// </summary>
        /// <param name="dimensionPoints"></param>
        public void UpdateInfoboxPosition(Vector3[] dimensionPoints)
        {
            if (infoboxWidth <= 0)
            {
                infoboxWidth = GetComponent<Renderer>().bounds.size.z;
                GetComponent<Collider>().enabled = false;
            }

            transform.position = ((new Vector3(dimensionPoints[1].x + dimensionPoints[7].x, dimensionPoints[1].y + dimensionPoints[7].y, dimensionPoints[1].z + dimensionPoints[7].z) / 2f));
            transform.localPosition -= new Vector3(0.0f, 0.0f, infoboxWidth / 2.0f);
        }
    }
}
