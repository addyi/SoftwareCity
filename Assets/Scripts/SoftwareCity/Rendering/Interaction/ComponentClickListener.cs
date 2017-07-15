using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.UI;

public class ComponentClickListener : MonoBehaviour, IInputClickHandler
{
    private static GameObject lastReference = null;
    private static Color lastColor;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if(lastReference != null)
        {
            lastReference.GetComponent<Renderer>().material.color = lastColor;
        }
        lastReference = this.gameObject;
        lastColor = this.gameObject.GetComponent<Renderer>().material.color;

        GameObject.FindGameObjectWithTag("Infobox").GetComponentInChildren<Text>().text = GetComponent<BaseInformation>().ToString();
        GetComponent<Renderer>().material.color = Color.yellow;
    }
}
