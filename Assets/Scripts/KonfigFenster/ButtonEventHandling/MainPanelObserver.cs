using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ConfigurationWindow.ButtonEventHandling
{
    public class MainPanelObserver : MonoBehaviour
    {
        //private ConfigurationObserver observer;
        private bool localProject;
        private Button[] allActiveButtons;
        private GameObject deactivateButton;
        private bool disabledButton;
        // Use this for initialization
        void Start()
        {
            //observer.GetLocalProject();
            allActiveButtons = GetComponentsInChildren<Button>();
            string externTag = GameObject.FindGameObjectWithTag("Extern").tag;
            localProject = GetLocalProject();
            CheckBeforeClick(externTag);
            AddingListener();
        }

        public void RefreshDisplay()
        {
            Debug.Log(disabledButton);
            if (disabledButton)
                DisableButton();
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

        private void AddingListener()
        {
            for(int i = 0; i < allActiveButtons.Length; i++)
            {
                int temp = i;
                allActiveButtons[i].onClick.AddListener(() => Clicked(temp));
            }
        }

        private void Clicked(int i)
        {
            
        }
    }
}
