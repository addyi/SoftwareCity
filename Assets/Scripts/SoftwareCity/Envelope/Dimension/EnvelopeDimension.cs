using UnityEngine;
using SoftwareCity.Infobox;
using SoftwareCity.Envelope.Interaction;

namespace SoftwareCity.Envelope.Dimension
{
    public class EnvelopeDimension : MonoBehaviour
    {
        /// <summary>
        /// Array which save all corner points from the cube.
        /// </summary>
        [SerializeField]
        private Vector3[] dimensionPoints;

        /// <summary>
        /// Save the reference of the scaler object producer.
        /// </summary>
        private ScaleObjectProducer scaleObjectProducer;

        /// <summary>
        /// Save the reference of the rotate object producer.
        /// </summary>
        private RotateObjectProducer rotateObjectProducer;

        /// <summary>
        /// Save the reference of the wire object producer.
        /// </summary>
        private WireObjectProducer wireObjectProducer;

        /// <summary>
        /// Save the reference of the infobox handler.
        /// </summary>
        private InfoboxHandler infoboxHandler;

        /// <summary>
        /// Array with the diretion vectors from the center of the cube.
        /// </summary>
        private readonly Vector3[] pointDirections = new Vector3[] {
            new Vector3(1, 1, 1),
            new Vector3(1, -1, -1),
            new Vector3(-1, 1, 1),
            new Vector3(-1, 1, -1),
            new Vector3(-1, -1, 1),
            new Vector3(1, 1, -1),
            new Vector3(1, -1, 1),
            new Vector3(-1, -1, -1)
        };

        void Start()
        {
            dimensionPoints = new Vector3[8];

            //get the reference to the producer components
            scaleObjectProducer = transform.parent.GetComponentInChildren<ScaleObjectProducer>();
            rotateObjectProducer = transform.parent.GetComponentInChildren<RotateObjectProducer>();
            wireObjectProducer = transform.parent.GetComponentInChildren<WireObjectProducer>();
            infoboxHandler = transform.parent.GetComponentInChildren<InfoboxHandler>();

            //update and calculate the dimension points
            UpdateDimensionPoints();
        }

        /// <summary>
        /// Update the whole envelope and the components around.
        /// </summary>
        public void UpdateDimensionPoints()
        {
            dimensionPoints[0] = CalcPointPosition(pointDirections[0]);
            dimensionPoints[1] = CalcPointPosition(pointDirections[1]);
            dimensionPoints[2] = CalcPointPosition(pointDirections[2]);
            dimensionPoints[3] = CalcPointPosition(pointDirections[3]);
            dimensionPoints[4] = CalcPointPosition(pointDirections[4]);
            dimensionPoints[5] = CalcPointPosition(pointDirections[5]);
            dimensionPoints[6] = CalcPointPosition(pointDirections[6]);
            dimensionPoints[7] = CalcPointPosition(pointDirections[7]);

            scaleObjectProducer.UpdateScalerPositions(dimensionPoints);
            rotateObjectProducer.UpdateRotatorPositions(dimensionPoints);
            wireObjectProducer.UpdateWirePositions(dimensionPoints);
            infoboxHandler.UpdateInfoboxPosition(dimensionPoints);
        }
        
        /// <summary>
        /// Calculate the new dimension points of the envelope.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        private Vector3 CalcPointPosition(Vector3 vector)
        {
            return this.gameObject.transform.TransformPoint(
                this.gameObject.transform.localPosition + new Vector3(
                    this.gameObject.GetComponent<MeshFilter>().mesh.bounds.extents.x * vector.x,
                    this.gameObject.GetComponent<MeshFilter>().mesh.bounds.extents.y * vector.y,
                    this.gameObject.GetComponent<MeshFilter>().mesh.bounds.extents.z * vector.z)
                    );
        }
    }
}
