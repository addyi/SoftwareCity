using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConfigurationWindow.ConfigurationObserver;
using UnityEngine.UI;
using DiskIO.AvailableMetrics;
using System.Linq;
using ConfigurationWindow.ButtonEventHandling.ReadFromPanel;
using UnityEngine.EventSystems;

namespace ConfigurationWindow.ButtonEventHandling.WriteOnPanel
{
    public class MetricButtonPool : MonoBehaviour
    {
        /// <summary>
        /// A reference to the button in the prefab directory.
        /// </summary>
        public GameObject buttonPrefab;
        /// <summary>
        /// A reference to put all buttons in the right position.
        /// </summary>
        public GameObject heightContent;
        /// <summary>
        /// A reference to the color label, to insert the buttons there.
        /// </summary>
        public GameObject colorContent;
        /// <summary>
        /// A reference to the pyramid label, to insert the buttons there.
        /// </summary>
        public GameObject pyramidContent;


        /// <summary>
        /// A reference to the script PanelExchanger, to switch between panels.
        /// </summary>
        private PanelExchanger panelHandler;
        /// <summary>
        /// The Orchestrator is there to get all availablemetrics and to inform the other components.
        /// </summary>
        private GameObject orchestrator;
        /// <summary>
        /// The inputhandler is there to add the names of an submit button and show them in the overviewpanel.
        /// </summary>
        private GameObject inputHandler;
        /// <summary>
        /// A reference to the script OverviewPanelResult, to show which button was clicked and summarize it.
        /// </summary>
        private GameObject showPanelResult;


        /// <summary>
        /// Saves the default metric for the area.
        /// </summary>
        private Metric saveAreaMetric;
        /// <summary>
        /// A list, which saves the metric from the last clicked button.
        /// </summary>
        private List<Metric> clickedMetrics;
        /// <summary>
        /// a list of metrics from the json file
        /// </summary>
        private List<Metric> _metricList;
        /// <summary>
        /// A sublist from the _metricList. In this list they are only metrics with percentage.
        /// </summary>
        private List<Metric> pyramidList;

        // Use this for initialization
        void Awake()
        {
            inputHandler = GameObject.FindGameObjectWithTag("SamplePanel");
            orchestrator = GameObject.FindGameObjectWithTag("Orchestrator");
            panelHandler = GetComponent<PanelExchanger>();
            showPanelResult = GameObject.FindGameObjectWithTag("OverviewPanel");


            clickedMetrics = new List<Metric>();
            GetMetricList();
            AddButtons("ColorPanel", heightContent.transform, _metricList);
            AddButtons("PyramidPanel", colorContent.transform, _metricList);
            AddButtons("OverviewPanel", pyramidContent.transform, pyramidList);
        }

        /// <summary>
        /// Divide the metric list into seperate list for the pyramid panel.
        /// </summary>
        private void GetMetricList()
        {
            _metricList = new List<Metric>();
            foreach (Metric metric in orchestrator.GetComponent<Orchestrator.Orchestrator>().GetAvailableMetrics())
            {
                _metricList.Add((Metric)metric.Clone());
            }

            saveAreaMetric = _metricList.Find(x => x.key.Equals("ncloc"));
            _metricList.Remove(saveAreaMetric);
            //TODO change percentege if merge with develop!!
            pyramidList = _metricList
                .Where( x => x.datatype.Equals("percentage")).ToList<Metric>();
            _metricList.RemoveAll(x => pyramidList.Contains(x));
        }

        /// <summary>
        /// Adding buttons in the panel.
        /// </summary>
        private void AddButtons(string panelTag, Transform panel, List<Metric>metricList)
        {
            foreach(Metric m in metricList)
            {
                GameObject metricButton = Instantiate<GameObject>(buttonPrefab);
                metricButton.transform.SetParent(panel.transform);
                metricButton.transform.localPosition = new Vector3(metricButton.transform.localPosition.x, metricButton.transform.localPosition.y, 0.0f);
                metricButton.transform.localScale = new Vector3(1f, 1f, 1f);
                metricButton.GetComponentInChildren<Text>().text = m.name;
                DisableImage(metricButton);
                metricButton.GetComponent<Button>().onClick.AddListener(() => Clicked(metricButton, m, panelTag));
            }
        }

        /// <summary>
        /// Disable the images in the buttons to not getting any conflicts between the buttons from different panels.
        /// </summary>
        /// <param name="metricButton"></param>
        private void DisableImage(GameObject metricButton)
        {
            metricButton.GetComponent<Image>().enabled = false;
            metricButton.GetComponentInChildren<Text>().enabled = false;
        }

        /// <summary>
        /// The listener for a button, to switch between panels and inform the orchestrator.
        /// </summary>
        /// <param name="m">An Metric object to collect them and afterwards inform the orchestrator.</param>
        void Clicked(GameObject clickedButton, Metric m, string panelTag )
        {
            if (panelTag.Equals("ColorPanel"))
            {
                DisableButton(m.name);
                inputHandler.GetComponent<InputManager>().InsertElement(saveAreaMetric.name);
                clickedMetrics.Add(saveAreaMetric);
            }

            inputHandler.GetComponent<InputManager>().InsertElement(m.name);
            clickedMetrics.Add(m);
            switch (panelTag)
            {
                case "PyramidPanel":
                    orchestrator.GetComponent<Orchestrator.Orchestrator>().SecondMetricSelected();
                    break;
                case "OverviewPanel":
                    showPanelResult.GetComponent<OverviewPanelResult>().WriteOnPanel();
                    orchestrator.GetComponent<Orchestrator.Orchestrator>().SelectMetrics(clickedMetrics.ToArray());
                    break;
            }
            panelHandler.NextPanel(panelTag);
        }

        /// <summary>
        /// Enables the button, after the panels get resetted.
        /// </summary>
        public void EnableButton()
        {
            Button[] activeButtons = colorContent.GetComponentsInChildren<Button>();
            foreach(Button b in activeButtons)
            {
                if (!b.interactable)
                    b.interactable = true;
            }
        }

        /// <summary>
        /// Disables a specific button.
        /// </summary>
        /// <param name="name">The name describes which button was pressed.</param>
        private void DisableButton(string name)
        {
            Button[] activeButtons = colorContent.GetComponentsInChildren<Button>();
            foreach (Button b in activeButtons)
            {
                if(b.GetComponentInChildren<Text>().text.Equals(name))
                {
                    b.interactable = false;
                }
            }
        }

        public void JumpBack()
        {
            if(MainPanelObserver.isLocal)
            {
                panelHandler.GetComponent<PanelExchanger>().NextPanel("MainPanel");
            } else
            {
                panelHandler.GetComponent<PanelExchanger>().NextPanel("SamplePanel");
            }
        }
    }
}
