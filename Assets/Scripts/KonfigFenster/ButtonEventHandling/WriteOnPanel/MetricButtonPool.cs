﻿using System.Collections;
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
        public GameObject colorContent;
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

        private GameObject currentPanel;

        private Metric saveAreaMetric;

        private List<Metric> savedMetrics;
        /// <summary>
        /// a list of metrics from the json file
        /// </summary>
        private List<Metric> _metricList;
        /// <summary>
        /// A sublist from the _metricList. In this list they are only metrics with percentage.
        /// </summary>
        private List<Metric> pyramidList;


        private bool isActive;
        // Use this for initialization
        void Awake()
        {

            this.currentPanel = GameObject.FindGameObjectWithTag("HeightPanel");
            inputHandler = GameObject.FindGameObjectWithTag("SamplePanel");
            orchestrator = GameObject.FindGameObjectWithTag("Orchestrator");
            panelHandler = GetComponent<PanelExchanger>();
            showPanelResult = GameObject.FindGameObjectWithTag("OverviewPanel");


            savedMetrics = new List<Metric>();
            GetMetricList();
            AddButtons("ColorPanel", heightContent.transform, _metricList);
            AddButtons("PyramidPanel", colorContent.transform, _metricList);
            AddButtons("OverviewPanel", pyramidContent.transform, pyramidList);
        }

        /// <summary>
        /// After every button submit the script should start the method RefreshDisplay() again to check which panel is now active.
        /// </summary>
        public void RefreshDisplay()
        {
            //TODO How to get the panel??
            currentPanel = EventSystem.current.currentSelectedGameObject.transform.root.gameObject;
            print(currentPanel);
        }

        /// <summary>
        /// Divide the metric list into seperate list for the pyramid panel.
        /// </summary>
        private void GetMetricList()
        {
            _metricList = orchestrator.GetComponent<Orchestrator.Orchestrator>().GetAvailableMetrics();
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
                savedMetrics.Add(saveAreaMetric);
            }

            inputHandler.GetComponent<InputManager>().InsertElement(m.name);
            savedMetrics.Add(m);
            switch (panelTag)
            {
                case "PyramidPanel":
                    orchestrator.GetComponent<Orchestrator.Orchestrator>().SecondMetricSelected();
                    break;
                case "OverviewPanel":
                    showPanelResult.GetComponent<OverviewPanelResult>().WriteOnPanel();
                    orchestrator.GetComponent<Orchestrator.Orchestrator>().SelectMetrics(savedMetrics.ToArray());
                    break;
            }
            panelHandler.NextPanel(panelTag);
        }

        public void EnableButton()
        {
            Button[] activeButtons = colorContent.GetComponentsInChildren<Button>();
            foreach(Button b in activeButtons)
            {
                if (!b.interactable)
                    b.interactable = true;
            }
        }

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
    }
}
