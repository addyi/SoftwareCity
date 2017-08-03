using ConfigurationWindow.DataStorage;
using DataModel.ProjectTree.Components;
using UnityEngine;
using UnityEngine.UI;

namespace ConfigurationWindow.ButtonEventHandling
{
    public class MainPanelObserver : MonoBehaviour
    {
        /// <summary>
        /// A reference to the GameObject where the Orchestrator script is, to call the methods from orchestrator.
        /// </summary>
        private GameObject orchestrator;

        /// <summary>
        /// A reference to the select local project button, to disable it afterwards, if needed.
        /// </summary>
        private GameObject deactivateButton;
        /// <summary>
        /// The ProjectComponent saves the local project from storage.
        /// </summary>
        private ProjectComponent localProject;
        /// <summary>
        /// An boolean to diable the loadLocalProjectButton.
        /// </summary>
        private bool disabledButton = false;
        /// <summary>
        /// If the loadLocalProjectButton was pressed, then it should change the correct panels, if the back button was pressed.
        /// </summary>
        public static bool isLocal;
        // Use this for initialization
        void Start()
        {

            orchestrator = GameObject.FindGameObjectWithTag("Orchestrator");
            //TODO Check if LocalProject exist.
            deactivateButton = GameObject.FindGameObjectWithTag("Extern");
            deactivateButton.GetComponent<Button>().interactable = false;
            RefreshDisplay();
        }

        /// <summary>
        /// The panel refreshes everytime it was reloaded, to call the method CheckBeforeClick.
        /// </summary>
        public void RefreshDisplay()
        {
            localProject = orchestrator.GetComponent<Orchestrator.Orchestrator>().GetLocalProject();
            CheckBeforeClick();
        }



        /// <summary>
        /// A listener for the button to commit the local project to the orchestrator.
        /// </summary>
        public void SelectLocalProject()
        {
            /*
            OverviewElements.InsertElement(localProject.Name);
            isLocal = true;
            orchestrator.GetComponent<Orchestrator.Orchestrator>().SelectLocalProject(localProject.Key);
            */
        }

        /// <summary>
        /// IF the loadOnlineProjectButton was pressed, then the value of isLocal is false.
        /// </summary>
        /// <param name="isOnline">The value for the boolean variable isLocal.</param>
        public void IsOnline(bool isOnline)
        {
            isLocal = isOnline;
            
        } 

        /// <summary>
        /// If an local project exists the button is enabled, otherwise disabled.
        /// </summary>
        private void CheckBeforeClick()
        {
            if (localProject == null)
            {
                DisableButton(disabledButton);
            }
            else
            {
                DisableButton();
            }
        }

        /// <summary>
        /// Disables the select local project button.
        /// </summary>
        private void DisableButton(bool disable = true)
        {
            /*
            deactivateButton.GetComponent<Button>().interactable = disable;
            */
        }

    }
}
