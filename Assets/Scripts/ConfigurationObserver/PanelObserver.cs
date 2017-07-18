using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ConfigurationWindow.ConfigurationObserver
{
    public class PanelObserver : MonoBehaviour
    {
        [SerializeField]
        private GameObject canvasParent;
        // Use this for initialization

        [SerializeField]
        private GameObject[] availablePanels;
        
        private void Start()
        {
            RenderringPanel(canvasParent, true);
        }

        public GameObject[] GetPanels()
        {
            return availablePanels;
        }

        public void RenderringPanel(GameObject panel, bool check)
        {
            Debug.Log(panel.ToString());
            foreach(Image image in panel.GetComponentsInChildren<Image>())
            {
                image.enabled = check;
            }

            foreach(Text text in panel.GetComponentsInChildren<Text>())
            {
                text.enabled = check;
            }
        }
    }
}
