using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConfigurationWindow.ConfigurationObserver;
using UnityEngine.UI;

namespace ConfigurationWindow.ButtonEventHandling.WriteOnPanel
{
    public class MetricButtonPool : MonoBehaviour
    {
        private GameObject panelParent;
        private GameObject metricPanel;
        

        private ConfigurationWindowObserver<string> metrics;
        private List<string> metricList;
        private PanelObserver panelHandler;
        public GameObject buttonPrefab;
        public GameObject panelScrollView;

        public GameObject actualPanel;
        public GameObject nextPanel;
        // Use this for initialization
        void Awake()
        {
            panelParent = GameObject.Find("ConfigurationCanvas");
            metricPanel = panelParent.transform.FindChild("ChoosePyramidPanel").gameObject;
            Debug.Log(metricPanel.ToString());

            metricList = new List<string> { "Lines Of Code", "Bugs", "Code Smells", "Issues", "Functions" };
            AddButtons();
        }

        private void AddButtons()
        {
            foreach (string s in metricList)
            {
                GameObject metricButton = Instantiate<GameObject>(buttonPrefab);
                metricButton.transform.SetParent(panelScrollView.transform);
                metricButton.transform.localPosition = new Vector3(metricButton.transform.localPosition.x, metricButton.transform.localPosition.y, 0.0f);
                metricButton.GetComponentInChildren<Text>().text = s;
                DisableImage(metricButton);
                metricButton.GetComponent<Button>().onClick.AddListener(Clicked);
            }
        }

        private void DisableImage(GameObject metricButton)
        {
            metricButton.GetComponent<Image>().enabled = false;
            metricButton.GetComponentInChildren<Text>().enabled = false;
        }

        void Clicked()
        {
            actualPanel.SetActive(false);
            nextPanel.SetActive(true);
        }
    }
}
