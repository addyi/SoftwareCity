using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ConfigurationWindow.DataStorage;

namespace ConfigurationWindow.ButtonEventHandling
{
    public class MetricsSummary : MonoBehaviour
    {

        /// <summary>
        /// An array of Buttons, to get all Buttons from the Panel;
        /// </summary>
        private Button[] allButtons;

        private bool jumpBack;

        private Button saveButton;
        // Use this for initialization
        void Start()
        {
            allButtons = GetComponentsInChildren<Button>();

            CheckBeforeRemove();

            allButtons[0].onClick.AddListener(BackButton);
            allButtons[1].onClick.AddListener(delegate { ButtonWasClicked(1); });
            allButtons[2].onClick.AddListener(delegate { ButtonWasClicked(2); });
            allButtons[3].onClick.AddListener(delegate { ButtonWasClicked(3); });
            allButtons[4].onClick.AddListener(delegate { ButtonWasClicked(4); });
            allButtons[5].onClick.AddListener(delegate { ButtonWasClicked(5); });

            //Destroy(allButtons[5].GetComponent<Button>());
        }

        void BackButton()
        {
            jumpBack = true;
            OverviewElements.RemoveElement(OverviewElements.Length() - 1);
        }

        /// <summary>
        /// Checks the panel, if one button has the same name as the actual one and removes it.
        /// </summary>
        void CheckBeforeRemove()
        {
            foreach (Button b in allButtons)
            {
                if (b.GetComponentInChildren<Text>().text.Equals(OverviewElements.GetElement(OverviewElements.Length() - 1)))
                {
                    b.gameObject.SetActive(false);
                    saveButton = b;
                }
            }
            if(saveButton != null)
                Debug.Log("Saved Button: " + saveButton.GetComponentInChildren<Text>().text);
            jumpBack = false;
        }

        //To Do: Check if the one button was pressed
        /// <summary>
        /// Checking which Button was pressed and insert it to the Datastorage.
        /// </summary>
        /// <param name="i">An index for the Button array.</param>
        void ButtonWasClicked(int i)
        {
            Debug.Log("Button was clicked: " + allButtons[i].GetComponentInChildren<Text>().text);
            OverviewElements.InsertElement(allButtons[i].GetComponentInChildren<Text>().text);
        }
    }
}