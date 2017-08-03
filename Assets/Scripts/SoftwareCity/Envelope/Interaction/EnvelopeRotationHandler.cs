using HoloToolkit.Unity.InputModule;
using UnityEngine;

namespace SoftwareCity.Envelope.Interaction
{
    public class EnvelopeRotationHandler : MonoBehaviour, IManipulationHandler
    {
        /// <summary>
        /// Speed of the rotation.
        /// </summary>
        [SerializeField]
        private float speed = 20.0f;

        /// <summary>
        /// Save the reference of the enviroment gameobject.
        /// </summary>
        private GameObject enviroment;

        /// <summary>
        /// Save the reference of the scaler gameobject.
        /// </summary>
        private GameObject scaler;

        void Start()
        {
            enviroment = GameObject.FindGameObjectWithTag("Enviroment");
            scaler = GameObject.FindGameObjectWithTag("Scaler");
        }

        /// <summary>
        /// If the manipulation canceled then remove listener and enable the colliders.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnManipulationCanceled(ManipulationEventData eventData)
        {    
            InputManager.Instance.RemoveGlobalListener(this.gameObject);
            ActivateCollider(true);
        }

        /// <summary>
        /// If the manipulation completed then remove listener and enable the colliders.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnManipulationCompleted(ManipulationEventData eventData)
        {
            InputManager.Instance.RemoveGlobalListener(this.gameObject);
            ActivateCollider(true);
        }

        /// <summary>
        /// If the manipulation started then add the listener and disable the colliders.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnManipulationStarted(ManipulationEventData eventData)
        {
            InputManager.Instance.AddGlobalListener(this.gameObject);
            ActivateCollider(false);
        }
        
        /// <summary>
        /// Handle the gaze input to rotate the enviroment.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnManipulationUpdated(ManipulationEventData eventData)
        {
            float multiplier = 1.0f;
            float cameraLocalYRotation = Camera.main.transform.localRotation.eulerAngles.y;

            if (cameraLocalYRotation > 270 || cameraLocalYRotation < 90)
                multiplier = -1.0f;

            var rotation = new Vector3(0.0f, eventData.CumulativeDelta.x * multiplier);
            enviroment.transform.Rotate(rotation * speed, Space.World);
        }

        /// <summary>
        /// Enable or disable the colliders fromt the scaler gameobject.
        /// </summary>
        /// <param name="b"></param>
        private void ActivateCollider(bool b)
        {
            foreach (Collider c in scaler.GetComponentsInChildren<Collider>())
            {
                c.enabled = b;
            }
        }
    }
}
