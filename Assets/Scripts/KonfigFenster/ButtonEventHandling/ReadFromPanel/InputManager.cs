using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ConfigurationWindow.DataStorage;
using System;

namespace ConfigurationWindow.ButtonEventHandling.ReadFromPanel
{
    public class InputManager : MonoBehaviour
    {
        /// <summary>
        /// An array, to save all buttons from the panel.
        /// </summary>
        private Button[] allButtons;

        /// <summary>
        /// Save the Button in the metric button, to deactivate in the next Panel.
        /// </summary>
        private static Button saveLastButton;

        /// <summary>
        /// Looking up, which Panel is now active.
        /// </summary>
        private string actualTag;

        /// <summary>
        /// To check if one button was clicked to know which button in the next label is to deactivate or to set it active again.
        /// </summary>
        private static bool wasRemoved, wasClicked;

        /// <summary>
        /// All possible tags in the configuration window in one array.
        /// </summary>
        private readonly string[] ALLPANELTAGS = { "SamplePanel", "HeightPanel", "ColorPanel", "PyramidPanel" };

        /// <summary>
        /// Initialize the datastorage and add Eventlistener in each Button.
        /// </summary>
        void Start()
        {
            allButtons = GetComponentsInChildren<Button>();
            //Debug.Log(allButtons.Length);
            actualTag = SearchTag();
            if(actualTag.Equals("SamplePanel") && OverviewElements.IsEmpty())
            {
                OverviewElements.Initialize();
            }
            //AddingListener();
            //AddingJumpBackListener();
        }

        private void OnGUI()
        {
            if (wasClicked)
            {
                switch (actualTag)
                {
                    case "HeightPanel":

                        break;
                    case "ColorPanel":
                        //CheckBeforeRemove();
                        //NotifySecondMetricSelected();
                        break;
                    case "PyramidPanel":

                        break;
                }
                wasClicked = false;
            }
        }

        /// <summary>
        /// Searching with the help of the array: ALLPANELTAGS, to look up which panel is active now.
        /// </summary>
        /// <returns>Returns the actual tag as string.</returns>
        private string SearchTag()
        {
            bool looping = true;
            string temp = "";
            for(int i = 0; i < ALLPANELTAGS.Length && looping; i++)
            {
                if(GameObject.FindGameObjectWithTag(ALLPANELTAGS[i]))
                {
                    temp = ALLPANELTAGS[i];
                    looping = false;
                }
            }
            return temp;
        }

        public void InsertElement(string name)
        {
            OverviewElements.InsertElement(name);
        }

        /// <summary>
        /// This method adds for every button an eventlistener to observe which one was clicked.
        /// </summary>
        /*
        void AddingListener()
        {
            for(int i = 1; i < allButtons.Length; i++)
            {
                int temp = i;
                allButtons[i].onClick.AddListener(() => ClickedButton(temp));
            }
        }

        /// <summary>
        /// The clicked button will save the information from the button in th OverviewElements data.
        /// </summary>
        /// <param name="i">An index of the button array.</param>
        void ClickedButton(int i)
        {
            Debug.Log(allButtons[i].GetComponentInChildren<Text>().text);
            OverviewElements.InsertElement(allButtons[i].GetComponentInChildren<Text>().text);
            wasClicked = true;
        }

        /// <summary>
        /// Special eventlistener for the back button.
        /// </summary>
        void AddingJumpBackListener()
        {
            allButtons[0].onClick.AddListener(JumpBack);
        }

        /// <summary>
        /// Delete the last entry from the OverviewElements data and get to the preview Panel.
        /// </summary>
        void JumpBack()
        {
            if (Array.Exists(ALLPANELTAGS, element => tag.Equals(element)))
                wasRemoved = false;

            if (OverviewElements.Length() > 0)
                OverviewElements.RemoveElement(OverviewElements.Length() - 1);
            if (saveLastButton != null && !saveLastButton.interactable && SearchTag().Equals("HeightPanel"))
                saveLastButton.interactable = true;
        }
        */
        /// <summary>
        /// Check all buttons in the metric Panel, before the one button get disabled.
        /// </summary>
        void CheckBeforeRemove()
        {
            if (wasRemoved)
            {
                saveLastButton.gameObject.SetActive(true);
            }
            else
            {
                foreach (Button b in allButtons)
                {
                    if (b.GetComponentInChildren<Text>().text.Equals(OverviewElements.GetElement(OverviewElements.Length() - 1)))
                    {
                        saveLastButton = b;
                        saveLastButton.interactable = false;
                        wasRemoved = true;
                    }
                }
            }
        }
    }
}
