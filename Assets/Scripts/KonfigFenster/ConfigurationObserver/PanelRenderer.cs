using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ConfigurationWindow.ConfigurationObserver
{
    public class PanelRenderer : MonoBehaviour
    {
        private void Start()
        {
            RenderringPanel(GameObject.FindGameObjectWithTag("MainPanel"), true);
        }
        
        /// <summary>
        /// Enables/Disable all images, text, button, inputfield, scrollrect and scrollbar in a panel.
        /// </summary>
        /// <param name="panel">A reference from the panel which should be enable/disable.</param>
        /// <param name="check">An boolean to enable or disable the panel.</param>
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

            foreach(InputField inputField in panel.GetComponentsInChildren<InputField>())
            {
                inputField.enabled = check;
            }

            foreach(ScrollRect scrollRect in panel.GetComponentsInChildren<ScrollRect>())
            {
                scrollRect.enabled = check;
            }

            foreach(Scrollbar scrollbar in panel.GetComponentsInChildren<Scrollbar>())
            {
                scrollbar.enabled = check;
            }
        }
    }
}
