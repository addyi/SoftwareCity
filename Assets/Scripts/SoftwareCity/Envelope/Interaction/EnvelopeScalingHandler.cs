using HoloToolkit.Unity.InputModule;
using UnityEngine;
using SoftwareCity.Envelope.Dimension;

namespace SoftwareCity.Envelope.Interaction
{
    public class EnvelopeScalingHandler : MonoBehaviour, IManipulationHandler
    {
        /// <summary>
        /// Save the reference of the envelope gameobject.
        /// </summary>
        private GameObject envelope;

        /// <summary>
        /// Save the reference of the rotator gameobject.
        /// </summary>
        private GameObject rotator;

        /// <summary>
        /// Scale value at the start.
        /// </summary>
        private float startScale;

        void Start()
        {
            envelope = GameObject.FindGameObjectWithTag("Envelope");
            startScale = envelope.transform.localScale.y;
            rotator = GameObject.FindGameObjectWithTag("Rotator");
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
        /// Handle the gaze input to scale the envelope.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnManipulationUpdated(ManipulationEventData eventData)
        {
            float multiplier = 0.01f;

            Vector3 currentLocalScale = envelope.transform.localScale;

            if (!(currentLocalScale.x + multiplier < 0.1f || currentLocalScale.y + multiplier < 0.1f || currentLocalScale.z + multiplier < 0.1f) || eventData.CumulativeDelta.y > 0.0f)
            {
                envelope.transform.localScale += new Vector3(multiplier, multiplier, multiplier) * eventData.CumulativeDelta.y;

                transform.parent.localPosition = new Vector3(0.0f, 0.15f + (envelope.transform.localScale.y - startScale) / 2.0f, 0.0f);
                envelope.GetComponent<EnvelopeDimension>().UpdateDimensionPoints();
            }
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
        /// If the manipulation canceled then remove listener and enable the colliders.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnManipulationCanceled(ManipulationEventData eventData)
        {     
            InputManager.Instance.RemoveGlobalListener(this.gameObject);
            ActivateCollider(true);
        }

        /// <summary>
        /// Enable or disable the colliders fromt the scaler gameobject.
        /// </summary>
        /// <param name="b"></param>
        private void ActivateCollider(bool b)
        {
            foreach (Collider c in rotator.GetComponentsInChildren<Collider>())
            {
                c.enabled = b;
            }
        }
    }
}
