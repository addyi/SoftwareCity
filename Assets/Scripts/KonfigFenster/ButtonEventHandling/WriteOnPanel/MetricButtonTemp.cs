using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConfigurationWindow.ButtonEventHandling.WriteOnPanel
{
    public class MetricButtonTemp : MonoBehaviour
    {

        public GameObject buttonPrefab;
        public GameObject panelScrollView;

        public GameObject actualPanel;
        public GameObject nextPanel;
        // Use this for initialization
        void Start()
        {

            GameObject metricButton = Instantiate<GameObject>(buttonPrefab);
            metricButton.transform.SetParent(panelScrollView.transform);
            metricButton.transform.localPosition = new Vector3(metricButton.transform.localPosition.x, metricButton.transform.localPosition.y, 0);
            //metricButton set Text to the actual Metric;
            //AddListener to the Buttons
        }
        
    }
}