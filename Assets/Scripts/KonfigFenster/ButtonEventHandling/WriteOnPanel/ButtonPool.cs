using ConfigurationWindow.ConfigurationObserver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ConfigurationWindow.ButtonEventHandling.WriteOnPanel
{
    public class ButtonPool : MonoBehaviour
    {
        /// <summary>
        /// A reference to the button template.
        /// </summary>
        public GameObject buttonPrefab;
        /// <summary>
        /// A reference for the Panel to insert the buttons.
        /// </summary>
        public GameObject panelScrollView;

        /// <summary>
        /// Shows an reference to the actual panel.
        /// </summary>
        public GameObject actualPanel;
        /// <summary>
        /// A reference to the next panel.
        /// </summary>
        public GameObject nextPanel;

        private PanelExchanger changePanel;

        //the list to load all projects from SonarQube
        private List<string> allProjects;
        // Use this for initialization
        void Awake()
        {
            changePanel = this.GetComponent<PanelExchanger>();
            //here the list will be loaded
            //example projects for test
            allProjects = new List<string> { "LocalProject", "Online Project", "Java Project", "Foo Project" };
            foreach (string s in allProjects)
            {
                GameObject button = Instantiate(buttonPrefab) as GameObject;
                button.transform.SetParent(panelScrollView.transform);
                button.transform.localPosition = new Vector3(button.transform.localPosition.x, button.transform.localPosition.y, 0.0f);
                button.GetComponentInChildren<Text>().text = s;
                DisableImage(button);
                button.GetComponent<Button>().onClick.AddListener(Clicked);
            }
        }

        private void DisableImage(GameObject button)
        {
            button.GetComponentInChildren<Image>().enabled = false;
            button.GetComponentInChildren<Text>().enabled = false;
        }

        /// <summary>
        /// The button listener to switch between panels.
        /// </summary>
        void Clicked()
        {
            changePanel.NextPanel("ProjectSample");
        }
    }
}
