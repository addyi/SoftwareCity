using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoftwareCity.Envelope.Interaction
{
    public class ScaleObjectProducer : MonoBehaviour
    {
        /// <summary>
        /// Save the reference of the scaling gameobjects.
        /// </summary>
        [SerializeField]
        private GameObject[] scaler;
        
        /// <summary>
        /// Material of the scaling gameobjects.
        /// </summary>
        [SerializeField]
        private Material envelopeMaterial;
        
        /// <summary>
        /// Scale of the scaling gameobjects.
        /// </summary>
        private readonly Vector3 scalerLocalScale = new Vector3(0.04f, 0.04f, 0.04f);

        void Start()
        {
            scaler = null;
        }
        
        /// <summary>
        /// Create new scaler and the scaling gameobjects.
        /// </summary>
        /// <param name="length"></param>
        private void CreateScaler(int length)
        {
            scaler = new GameObject[length];
            for (int i = 0; i < length; i++)
            {
                scaler[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                scaler[i].transform.localScale = scalerLocalScale;
                scaler[i].GetComponent<Collider>().enabled = false;
                scaler[i].GetComponent<MeshRenderer>().material = envelopeMaterial;
                scaler[i].transform.SetParent(this.gameObject.transform);
            }
        }

        /// <summary>
        /// Update the position of the scaling gameobjects.
        /// </summary>
        /// <param name="dimensionPoints"></param>
        public void UpdateScalerPositions(Vector3[] dimensionPoints)
        {
            if (scaler == null)
            {
                CreateScaler(dimensionPoints.Length);
            }

            for (int i = 0; i < dimensionPoints.Length; i++)
            {
                scaler[i].transform.position = dimensionPoints[i];
            }
        }
    }
}
