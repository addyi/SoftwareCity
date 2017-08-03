using UnityEngine;
using ConfigurationWindow.DataStorage;


namespace ConfigurationWindow.ButtonEventHandling.ReadFromPanel
{
    public class InputManager : MonoBehaviour
    {
        /// <summary>
        /// Initialize the datastorage and add Eventlistener in each Button.
        /// </summary>
        void Start()
        {
            OverviewElements.Initialize();
        }
        /// <summary>
        /// Removes an element from the OverviewElements, to show the correct order for the OverviewPanel.
        /// </summary>
        public void RemoveLastElement()
        {
            if(OverviewElements.Length() > 0)
            {
                OverviewElements.RemoveElement(OverviewElements.Length() - 1);
            }
            OverviewElements.Print();
        }
        /// <summary>
        /// Inserts the name of the button as a string in the OverviewElements data.
        /// </summary>
        /// <param name="name">The name of the clicked button.</param>
        public void InsertElement(string name)
        {
            OverviewElements.InsertElement(name);
        }
    }
}
