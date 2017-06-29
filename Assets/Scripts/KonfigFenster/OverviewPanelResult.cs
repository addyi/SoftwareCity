using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ConfigurationWindow.DataStore;
using System.Text.RegularExpressions;

public class OverviewPanelResult : MonoBehaviour {

    /// <summary>
    /// An array of Text is to get all the TextFields from the Overviewpanel.
    /// </summary>
    private Text[] overviewResult;

	// Use this for initialization
	void Start () {
        overviewResult = GetComponentsInChildren<Text>();
        Debug.Log(OverviewElements.GetElement(0));
        CheckString();
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
        for (int i= 0; i < overviewResult.Length; i++)
        {
            if(j < OverviewElements.Length())
                temp = OverviewElements.GetElement(j);
            dummy = overviewResult[i].text;
            if(dummy.StartsWith("Projectname:"))
            {
                //temp = temp.Trim();
                Debug.Log(temp);
                overviewResult[i].text = "Projektname:\t" + temp;
                j++;
            }
            if(dummy.StartsWith("Area:"))
            {
                overviewResult[i].text = "Area:" + tab + "bbb";
            }
            if(dummy.StartsWith("Height"))
            {
                overviewResult[i].text = "Height:" + tab + temp;
                j++;
            }
            if(dummy.StartsWith("Color"))
            {
                overviewResult[i].text = "Color:" + tab + temp;
                j++;
            }
            if(dummy.StartsWith("Pyramid"))
            {
                overviewResult[i].text = "Pyramid:" + tab + "eee";
                j++;
            }
            if(dummy.StartsWith("Square"))
            {
                overviewResult[i].text = "Square:" + tab + "ff";
            }
            if(dummy.StartsWith("Circle"))
            {
                overviewResult[i].text = "Circle:" + tab + "ggg";
            }
        }
    }
	
}
