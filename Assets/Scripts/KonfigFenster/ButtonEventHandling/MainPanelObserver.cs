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
        //private ConfigurationObserver observer;
        private GameObject orchestrator;


        private bool isAvailable;
        private GameObject deactivateButton;
        private ProjectComponent localProject;
        private bool disabledButton;
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

        public void RefreshDisplay()
        {
            Debug.Log(disabledButton);
            //if (disabledButton) 
                //DisableButton();
        }

        public void SelectLocalProject()
        {
            OverviewElements.InsertElement(localProject.Name);
            orchestrator.GetComponent<Orchestrator.Orchestrator>().SelectProject(localProject.Key);
        }

        private void CheckBeforeClick(string tag)
        {
            if(tag != null && localProject == null)
            {
                deactivateButton = GameObject.FindGameObjectWithTag(tag);
                DisableButton();
                disabledButton = true;
            }
        }


        private void DisableButton()
        {
            deactivateButton.GetComponent<Button>().interactable = false;
        }
    }
}
