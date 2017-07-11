using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ConfigurationWindow.ButtonEventHandling.WriteOnPanel
{
    public class ButtonPool : MonoBehaviour
    {

        public GameObject buttonPrefab;
        public GameObject panelScrollView;
        // Use this for initialization
        void Start()
        {
            GameObject button = Instantiate(buttonPrefab) as GameObject;
            button.transform.SetParent(panelScrollView.transform);
            Debug.Log(panelScrollView.transform.position.z);
        }


    }
}
