using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoftwareCity.Envelope.Interaction
{
    public class RotateObjectProducer : MonoBehaviour
    {
        /// <summary>
        /// Save the reference of the rotation gameobjects.
        /// </summary>
        [SerializeField]
        private GameObject[] rotator;
        
        /// <summary>
        /// Material of the rotation gameobjects.
        /// </summary>
        [SerializeField]
        private Material envelopeMaterial;

        /// <summary>
        /// The scale of the rotation gameobjects.
        /// </summary>
        private readonly Vector3 scalerLocalScale = new Vector3(0.04f, 0.04f, 0.04f);

        void Start()
        {
            rotator = null;
        }

        /// <summary>
        /// Create new rotator and the rotation gameobjects.
        /// </summary>
        /// <param name="length"></param>
        private void CreateRotator(int length)
        {
            rotator = new GameObject[length / 2];
            for (int i = 0; i < length / 2; i++)
            {
                rotator[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                rotator[i].transform.localScale = scalerLocalScale;
                rotator[i].GetComponent<Collider>().enabled = false;
                rotator[i].GetComponent<MeshRenderer>().material = envelopeMaterial;
                rotator[i].transform.SetParent(this.gameObject.transform);
            }
        }

        /// <summary>
        /// Update the position of the rotation gameobjects.
        /// </summary>
        public void UpdateRotatorPositions(Vector3[] dimensionPoints)
        {
            if (rotator == null)
            {
                CreateRotator(dimensionPoints.Length);
            }

            for (int i = 0; i < dimensionPoints.Length / 2; i++)
            {
                rotator[i].transform.position = new Vector3(dimensionPoints[i].x, this.gameObject.transform.position.y, dimensionPoints[i].z);
            }
        }
    }
}
