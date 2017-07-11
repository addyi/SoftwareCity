using UnityEngine;
using SoftwareCity.Envelope.Interaction;

namespace SoftwareCity.Infobox
{
    public class Infoboxfunction : MonoBehaviour
    {
        /// <summary>
        /// Is the envelope enable.
        /// </summary>
        private bool envelopeEnabled;

        /// <summary>
        /// Save the reference of the envelope components.
        /// </summary>
        private GameObject[] envelopeComponents;

        void Start()
        {
            envelopeEnabled = true;
            envelopeComponents = new GameObject[] {
                GameObject.FindGameObjectWithTag("Scaler"),
                GameObject.FindGameObjectWithTag("Rotator"),
                GameObject.FindGameObjectWithTag("Wireframe")
            };
        }
        
        /// <summary>
        /// Function to follow the current cursor.
        /// </summary>
        public void FollowCursor()
        {
            if (!envelopeEnabled)
            {
                ShowEnvelope();
            }
            this.gameObject.transform.parent.GetComponent<EnviromentFollowCursor>().DisableChildColliderRendererTextAndImage();
            
            this.gameObject.transform.parent.GetComponent<EnviromentFollowCursor>().enabled = true;
        }

        /// <summary>
        /// Function to enable/disable the envelope box around the city.
        /// </summary>
        public void ShowEnvelope()
        {
            envelopeEnabled = !envelopeEnabled;

            foreach (GameObject component in envelopeComponents)
            {
                foreach (Renderer componentRenderer in component.GetComponentsInChildren<Renderer>())
                {
                    componentRenderer.enabled = envelopeEnabled;
                }

                foreach (Collider componentCollider in component.GetComponentsInChildren<Collider>())
                {
                    componentCollider.enabled = envelopeEnabled;
                }
            }
        }
    }
}
