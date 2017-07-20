using HoloToolkit.Unity.InputModule;
using SoftwareCity.Rendering.Utils.Information;
using UnityEngine;
using UnityEngine.UI;

public class ComponentClickListener : MonoBehaviour, IInputClickHandler
{
    /// <summary>
    /// Save the last clicked gameobject.
    /// </summary>
    private static GameObject lastReference = null;

    /// <summary>
    /// Save the last color.
    /// </summary>
    private static Color lastColor;

    /// <summary>
    /// Set the previous default values on previous gameobject and coloring the new one.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnInputClicked(InputClickedEventData eventData)
    {
        if(lastReference != null)
        {
            lastReference.GetComponent<Renderer>().material.color = lastColor;
        }
        lastReference = this.gameObject;
        lastColor = this.gameObject.GetComponent<Renderer>().material.color;

        GameObject.FindGameObjectWithTag("Infobox").GetComponentInChildren<Text>().text = ContentInformation(GetComponent<BaseInformation>());
        GetComponent<Renderer>().material.color = Color.yellow;
    }

    /// <summary>
    /// Get the content informations and put these as content into the infobox.
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    private string ContentInformation(BaseInformation info)
    {
        if (info is FileInformation)
            return ((FileInformation)info).ToString();

        return info.ToString();
    }
}
