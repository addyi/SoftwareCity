using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ConfigurationWindow.DataStorage;
using System.Text.RegularExpressions;

namespace ConfigurationWindow.ButtonEventHandling
{
    public class OverviewPanelResult : MonoBehaviour
    {

        private static bool changeRequest;

        private Button backButton;

        /// <summary>
        /// An array of Text is to get all the TextFields from the Overviewpanel.
        /// </summary>
        private Text[] overviewResult;

        // Use this for initialization
        void Start()
        {
            overviewResult = GetComponentsInChildren<Text>();
            backButton = GameObject.FindGameObjectWithTag("BackButton").GetComponent<Button>();
            //Debug.Log(OverviewElements.GetElement(0));
            //OverviewElements.Print();
            backButton.onClick.AddListener(JumpBack);
            changeRequest = true;
        }

        private void OnGUI()
        {
            if (changeRequest)
            {
                CheckString();
                changeRequest = false;
            }
        }

        private void JumpBack()
        {
            if (OverviewElements.Length() > 0)
                OverviewElements.RemoveElement(OverviewElements.Length() - 1);
            changeRequest = true;

        }

        /// <summary>
        /// Checking the datastore elements and push them in the Overviewpanel in the textfield.
        /// </summary>
        void CheckString()
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
                    //temp = temp.Trim();
                    Debug.Log(temp);
                    overviewResult[i].text = "Projectname:\t" + temp;
                    j++;
                }
                if (dummy.StartsWith("Area:"))
                {
                    overviewResult[i].text = "Area:" + tab + "bbb";
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
                    Debug.Log(OverviewElements.Length());
                    Debug.Log(OverviewElements.GetElement(j));
                    overviewResult[i].text = "Pyramid:" + tab + temp;
                    j++;
                }
                if (dummy.StartsWith("Square"))
                {
                    overviewResult[i].text = "Square:" + tab + "ff";
                }
                if (dummy.StartsWith("Circle"))
                {
                    overviewResult[i].text = "Circle:" + tab + "ggg";
                }
            }
        }

    }
}
