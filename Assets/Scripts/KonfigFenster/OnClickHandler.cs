using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ConfigurationWindow.DataStore;

public class OnClickHandler : MonoBehaviour {

    private Button[] allButtons;  

    void Start()
    {
        OverviewElements.Initialize();
        allButtons = GetComponentsInChildren<Button>();

        //Debug.Log(allButtons.Length);
        //AddButtonListener();
        allButtons[1].onClick.AddListener(delegate { ButtonWasClicked(1); });
        allButtons[2].onClick.AddListener(delegate { ButtonWasClicked(2); });
        allButtons[3].onClick.AddListener(delegate { ButtonWasClicked(3); });
        RemoveButton();
    }

    void RemoveButton()
    {
        Debug.Log("Remove Button!");
        //Destroy(allButtons[3]);
        //allButtons[3].gameObject.SetActive(false);
    }

    void AddButtonListener()
    {
        for(int j = 1; j < allButtons.Length - 1; j++)
        {
            allButtons[j].onClick.AddListener(delegate { ButtonWasClicked(j); });
        }
    }

    void ButtonWasClicked(int i)
    {
        //Debug.Log(i);
        Debug.Log("You have clicked the button: " + allButtons[i].GetComponentInChildren<Text>().text);
        //InsertElement(allButtons[i].GetComponentInChildren<Text>().text);
        OverviewElements.InsertElement(allButtons[i].GetComponentInChildren<Text>().text);
        //view.InsertElement(allButtons[i].GetComponentInChildren<Text>().text);
        Debug.Log(OverviewElements.Length() + " | " + OverviewElements.GetElement(0));
    }
}
