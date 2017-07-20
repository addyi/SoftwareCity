using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ConfigurationWindow.ConfigurationObserver
{
    public class PanelRenderer : MonoBehaviour
    {
        /*
        [SerializeField]
        private GameObject canvasParent;
        // Use this for initialization
      
        [SerializeField]
        private GameObject[] availablePanels;
  */
        private void Start()
        {
            RenderringPanel(GameObject.FindGameObjectWithTag("MainPanel"), true);
        }
        /*
        public GameObject[] GetPanels()
        {
            return availablePanels;
        }
        */
        public void RenderringPanel(GameObject panel, bool check)
        {
            //Debug.Log(panel.ToString());
            foreach(Image image in panel.GetComponentsInChildren<Image>())
            {
                image.enabled = check;
            }

            foreach(Text text in panel.GetComponentsInChildren<Text>())
            {
                text.enabled = check;
            }

            foreach(Button button in panel.GetComponentsInChildren<Button>())
            {
                button.enabled = check;
            }
        }
    }
}
