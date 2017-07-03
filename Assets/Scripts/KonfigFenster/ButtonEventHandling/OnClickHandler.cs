using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ConfigurationWindow.DataStorage;

namespace ConfigurationWindow.ButtonEventHandling
{
    public class OnClickHandler : MonoBehaviour
    {
        /// <summary>
        /// An array, to save all buttons from the panel.
        /// </summary>
        private Button[] allButtons;

        /// <summary>
        /// Initialize the datastorage and add Eventlistener in each Button.
        /// </summary>
        void Start()
        {
            OverviewElements.Initialize();
            allButtons = GetComponentsInChildren<Button>();

            //Debug.Log(allButtons.Length);
            AddButtonListener();
            //allButtons[1].onClick.AddListener(delegate { ButtonWasClicked(1); });
            //allButtons[2].onClick.AddListener(delegate { ButtonWasClicked(2); });
            //allButtons[3].onClick.AddListener(delegate { ButtonWasClicked(3); });
            //RemoveButton();
            allButtons[3].tag = "Online";
        }

        /// <summary>
        /// Adding an Eventlistener in Buttons, to check which Button was pressed.
        /// </summary>
        void AddButtonListener()
        {
            foreach (Button b in allButtons)
            {
                b.onClick.AddListener(() =>
                {
                    ButtonWasClicked(b.tag);
                });
            }
        }

        /// <summary>
        /// To save the text from the actual clicked Button in the OverviewDataStorage.
        /// </summary>
        /// <param name="tag">Checking the Button with the tag.</param>
        void ButtonWasClicked(string tag)
        {
            Debug.Log(tag);
            Debug.Log("You have clicked the button: " + allButtons[0].GetComponentInChildren<Text>().text);
            //InsertElement(allButtons[i].GetComponentInChildren<Text>().text);
            OverviewElements.InsertElement(CheckTag(tag));
            //view.InsertElement(allButtons[i].GetComponentInChildren<Text>().text);
            Debug.Log(OverviewElements.Length() + " | " + OverviewElements.GetElement(0));
        }

        /// <summary>
        /// Checks the tag from the Button, to know which button was pressed.
        /// </summary>
        /// <param name="tag">Tag name of the button.</param>
        /// <returns>Return the text name of the button.</returns>
        string CheckTag(string tag)
        {
            string temp = "";
            bool checkIsTrue = false;
            int i = 0;
            while (i < allButtons.Length && !checkIsTrue)
            {
                if (tag.Equals(allButtons[i].tag))
                {
                    temp = allButtons[i].GetComponentInChildren<Text>().text;
                    checkIsTrue = true;
                }
                i++;
            }
            return temp;
        }
    }
}
