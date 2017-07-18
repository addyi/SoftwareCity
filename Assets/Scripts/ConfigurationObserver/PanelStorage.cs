using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConfigurationWindow.ConfigurationObserver
{
    public class PanelStorage
    {
        private Dictionary<string, GameObject> panelData;

        public PanelStorage()
        {
            panelData = new Dictionary<string, GameObject>();
        }

        public void InsertPanel(string key, GameObject panel)
        {
            panelData.Add(key, panel);
        }

        public GameObject GetPanel(string key)
        {
            return panelData[key];
        }

        public override string ToString()
        {
            string s = "";
            foreach(KeyValuePair<string, GameObject> entries in panelData)
            {
                s += " " + entries.Key;
            }
            return s;
        }
    }
}