using ConfigurationWindow.ButtonEventHandling.ReadFromPanel;
using ConfigurationWindow.ConfigurationObserver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Webservice.Response.Project;

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
        /// A reference to the scripte PanelExchanger.
        /// </summary>
        private PanelExchanger changePanel;

        /// <summary>
        /// Reference to the script InputManager for later to see the results on the Overviewpanel.
        /// </summary>
        private GameObject inserter;

        /// <summary>
        /// A reference to the Orchestrator script.
        /// </summary>
        private GameObject orchestrator;
        /// <summary>
        /// An container to get all buttons and afterwards to destroiy them all, if the panels switch between login panel and main panel.
        /// </summary>
        private GameObject[] allButtons;
        /// <summary>
        /// A list to save all online projects from the server.
        /// </summary>
        private List<SQProject> resetList;

        // Use this for initialization
        void Start()
        {
            inserter = GameObject.FindGameObjectWithTag("SamplePanel");
            orchestrator = GameObject.FindGameObjectWithTag("Orchestrator");
            changePanel = this.GetComponent<PanelExchanger>();
        }

        /// <summary>
        /// Adding buttons to the panel with the reference from the button prefab.
        /// </summary>
        /// <param name="listOfproject">A list with all projects from the server or one button from the local project.</param>
        public void AddButtons(List<SQProject> listOfproject)
        {
            allButtons = new GameObject[listOfproject.Count];
            Debug.Log("Can now add Buttons");
            resetList = listOfproject;
            int i = 0;
            foreach(SQProject project in resetList)
            {
                GameObject button = Instantiate<GameObject>(buttonPrefab);
                button.transform.SetParent(panelScrollView.transform);
                button.transform.localPosition = new Vector3(button.transform.localPosition.x, button.transform.localPosition.y, 0.0f);
                button.transform.localScale = new Vector3(1f, 1f, 1f);
                button.GetComponentInChildren<Text>().text = project.nm;
                button.GetComponent<Button>().onClick.AddListener(() => Clicked(project));
                allButtons[i] = button;
                i++;
            }
        }

        /// <summary>
        /// Destroing all Buttons in the SamplePanel, to not having any duplicate.
        /// </summary>
        public void RemoveButtons()
        {
            if (allButtons != null)
            {
                foreach (GameObject button in allButtons)
                {
                    Destroy(button);
                }
                resetList.Clear();
            }
        }

        /// <summary>
        /// The button listener to switch between panels and to call the orchestrator, which project button was pressed.
        /// </summary>
        void Clicked(SQProject project)
        {
            orchestrator.GetComponent<Orchestrator.Orchestrator>().SelectOnlineProject(project.k);
            inserter.GetComponent<InputManager>().InsertElement(project.nm);
            changePanel.NextPanel("HeightPanel");
        }
    }
}
