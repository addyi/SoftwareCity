using HoloToolkit.Unity.InputModule;
using UnityEngine;

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

            InputManager.Instance.PushModalInputHandler(this.gameObject);
        }
        
        /// <summary>
        /// Update the position of the enviroment.
        /// </summary>
        void FixedUpdate()
        {
            this.gameObject.transform.position = cursor.transform.position;
        }
        
        /// <summary>
        /// Plae the enviroment.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnInputClicked(InputClickedEventData eventData)
        {
            if (gazeManager.HitObject == null || !gazeManager.HitObject.tag.Equals("Button"))
            {
                EnableChildColliderAndRenderer();

                this.gameObject.GetComponent<EnviromentFollowCursor>().enabled = false;
            }
        }
        
        /// <summary>
        /// Enable the colliders and renderers from Scaler, Rotator, Infobox and SoftwareCity
        /// </summary>
        public void EnableChildColliderAndRenderer()
        {
            ActivateCollider(true);
            ActivateRenderer(true);
        }
        
        /// <summary>
        /// Disable the colliders and renderers from Scaler, Rotator, Infobox and SoftwareCity
        /// </summary>
        public void DisableChildColliderAndRenderer()
        {
            ActivateCollider(false);
            ActivateRenderer(false);
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

            infobox = GameObject.FindGameObjectWithTag("Infobox");
            foreach (Renderer renderer in infobox.gameObject.GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = b;
            }
        }
    }
}
