using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConfigurationWindow.ConfigurationObserver;
using UnityEngine.UI;
using DiskIO.AvailableMetrics;
using System.Linq;
using ConfigurationWindow.ButtonEventHandling.ReadFromPanel;

namespace ConfigurationWindow.ButtonEventHandling.WriteOnPanel
{
    public class MetricButtonPool : MonoBehaviour
    {
        public GameObject buttonPrefab;
        public GameObject panelScrollView;

        private PanelExchanger panelHandler;
        private GameObject orchestrator;
        private GameObject inputHandler;


        private List<Metric> _metricList;
        private List<Metric> pyramidList;
        
        // Use this for initialization
        void Awake()
        {
            inputHandler = GameObject.FindGameObjectWithTag("SamplePanel");
            orchestrator = GameObject.FindGameObjectWithTag("Orchestrator");
            panelHandler = GetComponent<PanelExchanger>();
            GetMetricList();
            AddButtons();
        }

        /// <summary>
        /// Divide the metric list into seperate list for the pyramid panel.
        /// </summary>
        private void GetMetricList()
        {
            _metricList = orchestrator.GetComponent<Orchestrator.Orchestrator>().GetAvailableMetrics();
            //TODO change percentege if merge with develop!!
            pyramidList = _metricList
                .Where( x => x.datatype.Equals("percentege")).ToList<Metric>();
            _metricList.RemoveAll(x => pyramidList.Contains(x));
        }

        /// <summary>
        /// Adding buttons in the panel.
        /// </summary>
        private void AddButtons()
        {
            foreach(Metric m in _metricList)
            {
                GameObject metricButton = Instantiate<GameObject>(buttonPrefab);
                metricButton.transform.SetParent(panelScrollView.transform);
                metricButton.transform.localPosition = new Vector3(metricButton.transform.localPosition.x, metricButton.transform.localPosition.y, 0.0f);
                metricButton.transform.localScale = new Vector3(1f, 1f, 1f);
                metricButton.GetComponentInChildren<Text>().text = m.name;
                DisableImage(metricButton);
                metricButton.GetComponent<Button>().onClick.AddListener(() => Clicked(m));
            }
            /*
            foreach (string s in metricList)
            {
                GameObject metricButton = Instantiate<GameObject>(buttonPrefab);
                metricButton.transform.SetParent(panelScrollView.transform);
                metricButton.transform.localPosition = new Vector3(metricButton.transform.localPosition.x, metricButton.transform.localPosition.y, 0.0f);
                metricButton.transform.localScale = new Vector3(1f, 1f, 1f);
                metricButton.GetComponentInChildren<Text>().text = s;
                DisableImage(metricButton);
                metricButton.GetComponent<Button>().onClick.AddListener(Clicked);
            }
            */
        }

        private void DisableImage(GameObject metricButton)
        {
            metricButton.GetComponent<Image>().enabled = false;
            metricButton.GetComponentInChildren<Text>().enabled = false;
        }

        void Clicked(Metric m)
        {
            inputHandler.GetComponent<InputManager>().InsertElement(m.name);
            panelHandler.NextPanel("ColorPanel");
        }
    }
}
