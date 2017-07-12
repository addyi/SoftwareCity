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

        public GameObject actualPanel;
        public GameObject nextPanel;

        //the list to load all projects from SonarQube
        private List<string> allProjects;
        // Use this for initialization
        void Awake()
        {
            //here the list will be loaded
            //example projects for test
            allProjects = new List<string> { "LocalProject", "Online Project", "Java Project", "Foo Project" };
            foreach (string s in allProjects)
            {
                GameObject button = Instantiate(buttonPrefab) as GameObject;
                button.transform.SetParent(panelScrollView.transform);
                button.transform.localPosition = new Vector3(button.transform.localPosition.x, button.transform.localPosition.y, 0.0f);
                button.GetComponentInChildren<Text>().text = s;
                button.GetComponent<Button>().onClick.AddListener(Clicked);
            }
        }

        void Clicked()
        {
            actualPanel.SetActive(false);
            nextPanel.SetActive(true);
        }
    }
}
