using UnityEngine;
using ConfigurationWindow.ButtonEventHandling;
using UnityEngine.EventSystems;

namespace ConfigurationWindow.ConfigurationObserver
{
    public class PanelExchanger : MonoBehaviour
    {
        /// <summary>
        /// A reference to the script Panlerenderer, it deactivates/activate all images, buttonscripts, text and scrollviews.
        /// </summary>
        private PanelRenderer panelRenderer;


        private void Start()
        {
            panelRenderer = GetComponentInParent<PanelRenderer>();
        }
        
        /// <summary>
        /// Changes the panels.
        /// </summary>
        /// <param name="nextPanelTag">An tag to find the next panel.</param>
        public void NextPanel(string nextPanelTag)
        {
            //print(EventSystem.current.currentSelectedGameObject);
            //print(EventSystem.current.currentSelectedGameObject.transform.root.gameObject);
            
            panelRenderer.RenderringPanel(EventSystem.current.currentSelectedGameObject.transform.root.gameObject, false);
            panelRenderer.RenderringPanel(GameObject.FindGameObjectWithTag(nextPanelTag), true);
        }
    }
}
