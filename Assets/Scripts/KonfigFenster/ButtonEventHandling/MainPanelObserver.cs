using ConfigurationWindow.DataStorage;
using DataModel.ProjectTree.Components;
using System.Collections;
using System.Collections.Generic;
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
        private bool disabledButton;

        public static bool isLocal;
        // Use this for initialization
        void Start()
        {
            orchestrator = GameObject.FindGameObjectWithTag("Orchestrator");
            //TODO Check if LocalProject exist.
            localProject = orchestrator.GetComponent<Orchestrator.Orchestrator>().GetLocalProject();
            string externTag = GameObject.FindGameObjectWithTag("Extern").tag;
            CheckBeforeClick(externTag);
            //AddingListener();
        }

        /// <summary>
        /// A listener for the button to commit the local project to the orchestrator.
        /// </summary>
        public void SelectLocalProject()
        {
            OverviewElements.InsertElement(localProject.Name);
            isLocal = true;
            orchestrator.GetComponent<Orchestrator.Orchestrator>().SelectLocalProject(localProject.Key);
        }

        public void IsOnline(bool isOnline)
        {
            isLocal = isOnline;
            
        } 

        /// <summary>
        /// If an local project exists the button is enabled, otherwise disabled.
        /// </summary>
        /// <param name="tag"></param>
        private void CheckBeforeClick(string tag)
        {
            if(tag != null && localProject == null)
            {
                deactivateButton = GameObject.FindGameObjectWithTag(tag);
                DisableButton();
                disabledButton = true;
            }
        }

        /// <summary>
        /// Disables the select local project button.
        /// </summary>
        private void DisableButton()
        {
            deactivateButton.GetComponent<Button>().interactable = false;
        }
    }
}
