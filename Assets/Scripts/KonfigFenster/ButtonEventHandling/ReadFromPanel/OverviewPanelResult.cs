﻿using System.Collections;
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

        // Use this for initialization
        void Start()
        {
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
            string tab = "\t\t";
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
                    overviewResult[i].text = "Area:\t" + tab + temp;
                    j++;
                }
                if (dummy.StartsWith("Height"))
                {
                    overviewResult[i].text = "Height:\t" + tab + temp;
                    j++;
                }
                if (dummy.StartsWith("Color"))
                {
                    overviewResult[i].text = "Color:\t" + tab + temp;
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
