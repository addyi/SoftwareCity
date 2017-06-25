using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace SoftwareCity.Envelope.Interaction
{
    public class WireObjectProducer : MonoBehaviour
    {
        /// <summary>
        /// Save the reference of the wire gameobjects.
        /// </summary>
        [SerializeField]
        private GameObject[] wires;

        /// <summary>
        /// Material of the wireframe.
        /// </summary>
        [SerializeField]
        private Material envelopeMaterial;
        
        /// <summary>
        /// Sclae of the wire gameobjects.
        /// </summary>
        private readonly Vector3 scalerLocalScale = new Vector3(0.01f, 0.01f, 0.01f);

        void Start()
        {
            wires = null;
        }
        
        /// <summary>
        /// Create new wireframe and the wire gameobjects.
        /// </summary>
        /// <param name="length"></param>
        private void CreateWires(int length)
        {
            wires = new GameObject[length];
            for (int i = 0; i < length; i++)
            {
                wires[i] = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                wires[i].transform.localScale = scalerLocalScale;
                Destroy(wires[i].GetComponent<Collider>());
                wires[i].GetComponent<MeshRenderer>().material = envelopeMaterial;
                wires[i].GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.Off;
                wires[i].transform.SetParent(this.gameObject.transform);
            }
        }
        
        /// <summary>
        /// Update the position of the wire gameobjects.
        /// </summary>
        /// <param name="dimensionPoints"></param>
        public void UpdateWirePositions(Vector3[] dimensionPoints)
        {
            if (wires == null)
            {
                CreateWires(dimensionPoints.Length + 4);
            }

            CalcWirePosition(dimensionPoints[0], dimensionPoints[2], wires[0], new Vector3(0.0f, 0.0f, 90.0f));
            CalcWirePosition(dimensionPoints[0], dimensionPoints[5], wires[1], new Vector3(0.0f, 90.0f, 90.0f));
            CalcWirePosition(dimensionPoints[0], dimensionPoints[6], wires[2], new Vector3(0.0f, 0.0f, 0.0f));
            CalcWirePosition(dimensionPoints[2], dimensionPoints[3], wires[3], new Vector3(0.0f, 90.0f, 90.0f));
            CalcWirePosition(dimensionPoints[2], dimensionPoints[4], wires[4], new Vector3(0.0f, 0.0f, 0.0f));
            CalcWirePosition(dimensionPoints[3], dimensionPoints[5], wires[5], new Vector3(0.0f, 0.0f, 90.0f));
            CalcWirePosition(dimensionPoints[3], dimensionPoints[7], wires[6], new Vector3(0.0f, 0.0f, 0.0f));
            CalcWirePosition(dimensionPoints[4], dimensionPoints[6], wires[7], new Vector3(0.0f, 0.0f, 90.0f));
            CalcWirePosition(dimensionPoints[4], dimensionPoints[7], wires[8], new Vector3(0.0f, 90.0f, 90.0f));
            CalcWirePosition(dimensionPoints[1], dimensionPoints[7], wires[9], new Vector3(0.0f, 0.0f, 90.0f));
            CalcWirePosition(dimensionPoints[1], dimensionPoints[6], wires[10], new Vector3(0.0f, 90.0f, 90.0f));
            CalcWirePosition(dimensionPoints[1], dimensionPoints[5], wires[11], new Vector3(0.0f, 0.0f, 0.0f));
        }
        
        /// <summary>
        /// Calculate the length of the wires.
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="wire"></param>
        /// <param name="rotation"></param>
        private void CalcWirePosition(Vector3 startPoint, Vector3 endPoint, GameObject wire, Vector3 rotation)
        {
            float dist = Vector3.Distance(startPoint, endPoint);

            wire.transform.localScale = new Vector3(0.01f, dist * 5f / 2f, 0.01f);
            wire.transform.position = new Vector3(startPoint.x + endPoint.x, startPoint.y + endPoint.y, startPoint.z + endPoint.z) / 2f;

            wire.transform.localEulerAngles = new Vector3(rotation.x, rotation.y, rotation.z);

            wire.transform.SetParent(this.gameObject.transform);
        }
    }
}