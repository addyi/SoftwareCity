using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ConfigurationWindow.DataStorage;
using System;

namespace ConfigurationWindow.ButtonEventHandling
{
    public class OnClickHandler : MonoBehaviour
    {
        /// <summary>
        /// An array, to save all buttons from the panel.
        /// </summary>
        private Button[] allButtons;

        private static Button saveLastButton;

        private GameObject actualTag;

        private bool wasRemoved;

        private readonly string JUMPBACK = "BackButton";

        private readonly string[] ALLPANELTAGS = { "SamplePanel", "HeightPanel", "ColorPanel", "PyramidPanel" };

        private static int actualTagCounter;

        /// <summary>
        /// Initialize the datastorage and add Eventlistener in each Button.
        /// </summary>
        void Start()
        {
            allButtons = GetComponentsInChildren<Button>();
            if (actualTagCounter < ALLPANELTAGS.Length)
                actualTag = GameObject.FindGameObjectWithTag(ALLPANELTAGS[actualTagCounter]);
            else
                Debug.LogError("IndexOutOfBounceException in array: " + ALLPANELTAGS.Length);
            switch(actualTag.tag)
            {
                case "SamplePanel":
                    OverviewElements.Initialize();
                    Debug.Log("Initialize the beginning!| Counter: " +actualTagCounter);
                    break;
                case "HeightPanel":

                    break;
                case "ColorPanel":
                    CheckBeforeRemove();
                    break;
                case "PyramidPanel":

                    break;
            }
            AddingListener();
            AddingJumpBackListener();
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
            if(actualTagCounter < ALLPANELTAGS.Length)
                actualTagCounter++;
        }

        void AddingJumpBackListener()
        {
            allButtons[0].onClick.AddListener(BackButton);
        }

        void BackButton()
        {
            if (Array.Exists(ALLPANELTAGS, element => tag.Equals(element)))
                wasRemoved = false;

            if (OverviewElements.Length() > 0 && actualTagCounter > 0)
            {
                OverviewElements.RemoveElement(OverviewElements.Length() - 1);
                actualTagCounter--;
            }
            Start();
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
                        Debug.Log(saveLastButton.GetComponentInChildren<Text>().text);
                        b.gameObject.SetActive(false);
                        wasRemoved = true;
                    }
                }
            }
        }
    }
}
