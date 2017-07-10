using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ConfigurationWindow.DataStorage;
using System;

namespace ConfigurationWindow.ButtonEventHandling.ReadFromPanel
{
    public class OnClickHandler : MonoBehaviour
    {
        /// <summary>
        /// An array, to save all buttons from the panel.
        /// </summary>
        private Button[] allButtons;

        private static Button saveLastButton;

        private string actualTag;

        private static bool wasRemoved, wasClicked;

        private readonly string JUMPBACK = "BackButton";

        private readonly string[] ALLPANELTAGS = { "SamplePanel", "HeightPanel", "ColorPanel", "PyramidPanel" };

        /// <summary>
        /// Initialize the datastorage and add Eventlistener in each Button.
        /// </summary>
        void Start()
        {
            allButtons = GetComponentsInChildren<Button>();
            actualTag = SearchTag();
            if(actualTag.Equals("SamplePanel") && OverviewElements.IsEmpty())
            {
                OverviewElements.Initialize();
            }
            AddingListener();
            AddingJumpBackListener();
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
                        CheckBeforeRemove();
                        break;
                    case "PyramidPanel":

                        break;
                }
                wasClicked = false;
            }
        }

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

        void AddingListener()
        {
            for(int i = 1; i < allButtons.Length; i++)
            {
                int temp = i;
                allButtons[i].onClick.AddListener(() => ClickedButton(temp));
            }
        }

        void ClickedButton(int i)
        {
            Debug.Log(allButtons[i].GetComponentInChildren<Text>().text);
            OverviewElements.InsertElement(allButtons[i].GetComponentInChildren<Text>().text);
            wasClicked = true;
        }

        void AddingJumpBackListener()
        {
            allButtons[0].onClick.AddListener(JumpBack);
        }

        void JumpBack()
        {
            if (Array.Exists(ALLPANELTAGS, element => tag.Equals(element)))
                wasRemoved = false;

            if (OverviewElements.Length() > 0)
                OverviewElements.RemoveElement(OverviewElements.Length() - 1);
            if (saveLastButton != null && !saveLastButton.interactable && SearchTag().Equals("HeightPanel"))
                saveLastButton.interactable = true;
        }

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
