using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ConfigurationWindow.ButtonEventHandling
{
    public class MainPanelObserver : MonoBehaviour
    {
        //private ConfigurationObserver observer;
        private Orchestrator.Orchestrator orchestrator;


        private bool localProject;
        private GameObject deactivateButton;
        private bool disabledButton;
        // Use this for initialization
        void Start()
        {
            orchestrator = GetComponentInParent<Orchestrator.Orchestrator>();
            //TODO Check if LocalProject exist.
            //orchestrator.GetLocalProject();
            string externTag = GameObject.FindGameObjectWithTag("Extern").tag;
            localProject = GetLocalProject();
            //CheckBeforeClick(externTag);
            //AddingListener();
        }

        public void RefreshDisplay()
        {
            Debug.Log(disabledButton);
            //if (disabledButton) 
                //DisableButton();
        }

        private bool GetLocalProject()
        {
            return false;
        }

        private void CheckBeforeClick(string tag)
        {
            if(tag != null && !localProject)
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
