using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ConfigurationWindow.DataStorage;
using System;
using ConfigurationWindow.ConfigurationObserver;

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

        public void RemoveLastElement()
        {
            if(OverviewElements.Length() > 0)
            {
                OverviewElements.RemoveElement(OverviewElements.Length() - 1);
            }
            OverviewElements.Print();
        }

        public void InsertElement(string name)
        {
            OverviewElements.InsertElement(name);
        }
    }
}
