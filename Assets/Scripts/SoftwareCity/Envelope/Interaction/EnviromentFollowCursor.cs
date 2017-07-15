using HoloToolkit.Unity.InputModule;
using SoftwareCity.Rendering;
using UnityEngine;
using UnityEngine.UI;

namespace SoftwareCity.Envelope.Interaction
{
    public class EnviromentFollowCursor : MonoBehaviour, IInputClickHandler
    {
        /// <summary>
        /// Save the reference of the current cursor.
        /// </summary>
        private GameObject cursor;
        
        /// <summary>
        /// Save the reference of the gaze manager.
        /// </summary>
        private GazeManager gazeManager;
        
        /// <summary>
        /// Save the reference of the envelope gameobject.
        /// </summary>
        private GameObject envelope;
        
        /// <summary>
        /// Save the reference of the infobox gameobject.
        /// </summary>
        private GameObject infobox;
        
        void Start()
        {
            cursor = GameObject.FindGameObjectWithTag("Cursor");
            gazeManager = GameObject.FindGameObjectWithTag("InputManager").GetComponent<GazeManager>();
            envelope = GameObject.FindGameObjectWithTag("Envelope");

            InputManager.Instance.PushModalInputHandler(this.gameObject);
        }
        
        /// <summary>
        /// Update the position of the enviroment.
        /// </summary>
        void FixedUpdate()
        {
            this.gameObject.transform.position = cursor.transform.position + new Vector3(0.0f, envelope.GetComponentInChildren<SoftwareCityBuilder>().GetHeight() * 0.04f, 0.0f);
            //this.gameObject.transform.position = cursor.transform.position;
        }
        
        /// <summary>
        /// Plae the enviroment.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnInputClicked(InputClickedEventData eventData)
        {
            if (gazeManager.HitObject == null || !gazeManager.HitObject.tag.Equals("Button"))
            {
                EnableChildColliderRendererTextAndImage();

                this.gameObject.GetComponent<EnviromentFollowCursor>().enabled = false;
            }
        }
        
        /// <summary>
        /// Enable the colliders and renderers from Scaler, Rotator, Infobox and SoftwareCity
        /// </summary>
        public void EnableChildColliderRendererTextAndImage()
        {
            ActivateCollider(true);
            ActivateRenderer(true);
            ActivateText(true);
            ActivateImage(true);
        }
        
        /// <summary>
        /// Disable the colliders and renderers from Scaler, Rotator, Infobox and SoftwareCity
        /// </summary>
        public void DisableChildColliderRendererTextAndImage()
        {
            ActivateCollider(false);
            ActivateRenderer(false);
            ActivateText(false);
            ActivateImage(false);
        }
        
        /// <summary>
        /// Search and enable the colliders of the specific gameObjects
        /// </summary>
        private void ActivateCollider(bool b)
        {
            foreach (Collider collider in this.gameObject.GetComponentsInChildren<Collider>())
            {
                collider.enabled = b;
            }
        }
        
        /// <summary>
        /// Search and enable the renderers of the specific gameObjects
        /// </summary>
        private void ActivateRenderer(bool b)
        {
            envelope = GameObject.FindGameObjectWithTag("Envelope");
            foreach (Renderer renderer in envelope.gameObject.GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = b;
            }
        }

        /// <summary>
        /// Search and enable the texts of the specific gameObjects
        /// </summary>
        private void ActivateText(bool b)
        {
            infobox = GameObject.FindGameObjectWithTag("Infobox");
            foreach (Text text in infobox.gameObject.GetComponentsInChildren<Text>())
            {
                text.enabled = b;
            }
        }

        /// <summary>
        /// Search and enable the ímages of the specific gameObjects
        /// </summary>
        private void ActivateImage(bool b)
        {
            infobox = GameObject.FindGameObjectWithTag("Infobox");
            foreach (Renderer renderer in infobox.gameObject.GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = b;
            }
        }
    }
}
