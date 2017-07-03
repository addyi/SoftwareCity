using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ConfigurationWindow.DataStorage;

namespace ConfigurationWindow.ButtonEventHandling
{
    public class PyramidMetric : MonoBehaviour
    {

        private Button[] allButtons;

        // Use this for initialization
        void Start()
        {
            allButtons = GetComponentsInChildren<Button>();

            allButtons[1].onClick.AddListener(delegate { ButtonWasClicked(1); });
            allButtons[2].onClick.AddListener(delegate { ButtonWasClicked(2); });
            allButtons[3].onClick.AddListener(delegate { ButtonWasClicked(3); });
        }

        void ButtonWasClicked(int i)
        {
            
            OverviewElements.InsertElement(allButtons[i].GetComponentInChildren<Text>().text);
            Debug.Log("You pressed: " + OverviewElements.GetElement(0));
        }
    }
}
