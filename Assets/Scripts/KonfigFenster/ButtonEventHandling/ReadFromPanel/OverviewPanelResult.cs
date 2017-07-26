using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ConfigurationWindow.DataStorage;
using System.Text.RegularExpressions;

namespace ConfigurationWindow.ButtonEventHandling.ReadFromPanel
{
    public class OverviewPanelResult : MonoBehaviour
    {

        /// <summary>
        /// An array of Text is to get all the TextFields from the Overviewpanel.
        /// </summary>
        private Text[] overviewResult;
        /// <summary>
        /// A reference to the GameObject where the orchestrator script is, to call the methods from orchestrator. 
        /// </summary>
        private GameObject orchestrator;

        // Use this for initialization
        void Start()
        {
            orchestrator = GameObject.FindGameObjectWithTag("Orchestrator");
            overviewResult = GetComponentsInChildren<Text>();
        }


        /// <summary>
        /// Removes all elements, which shows which button was pressed, from the list.
        /// </summary>
        public void ResetList()
        {
            OverviewElements.ClearList();
        }

        /// <summary>
        /// Checking the datastore elements and push them in the Overviewpanel in the textfield.
        /// </summary>
        public void WriteOnPanel()
        {
            string dummy;
            string tab = "\t\t\t";
            string temp = "";
            int j = 0;
            for (int i = 0; i < overviewResult.Length; i++)
            {
                if (j < OverviewElements.Length())
                    temp = OverviewElements.GetElement(j);
                dummy = overviewResult[i].text;
                if (dummy.StartsWith("Projectname:"))
                { 
                    overviewResult[i].text = "Projectname:\t" + temp;
                    j++;
                }
                if (dummy.StartsWith("Area:"))
                {
                    overviewResult[i].text = "Area:" + tab + temp;
                    j++;
                }
                if (dummy.StartsWith("Height"))
                {
                    overviewResult[i].text = "Height:" + tab + temp;
                    j++;
                }
                if (dummy.StartsWith("Color"))
                {
                    overviewResult[i].text = "Color:" + tab + temp;
                    j++;
                }
                if (dummy.StartsWith("Pyramid"))
                {
                    overviewResult[i].text = "Pyramid:" + tab + temp;
                    j++;
                }
            }
        }

    }
}
