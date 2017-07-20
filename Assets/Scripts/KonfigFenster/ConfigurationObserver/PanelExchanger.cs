using UnityEngine;
using ConfigurationWindow.ButtonEventHandling;
using UnityEngine.EventSystems;

namespace ConfigurationWindow.ConfigurationObserver
{
    public class PanelExchanger : MonoBehaviour
    {
        private PanelRenderer panelRenderer;

       // private PanelStorage mapOfPanels;

        [SerializeField]
        private MainPanelObserver mainPanel;

       // private GameObject[] availablePanels;
        //private readonly string[] ALLPANELS =
        //    {"MainPanel", "LoginPanel", "ProjectSample", "ChooseHeightPanel", "ChooseColorPanel", "ChoosePyramidPanel", "OverviewPanel"};


        private void Start()
        {
            //mapOfPanels = new PanelStorage();
            //mainPanel = this.GetComponentInChildren<MainPanelObserver>();
            panelRenderer = GetComponentInParent<PanelRenderer>();
            //availablePanels = panelObserver.GetPanels();
           // InsertElements();
           // Debug.Log(mapOfPanels.GetPanel("MainPanel"));

            
        }
        /*
        private void InsertElements()
        {
            for(int i = 0; i  < ALLPANELS.Length; i++)
            {
                mapOfPanels.InsertPanel(ALLPANELS[i], availablePanels[i]);
            }
        }
        */
        /*
        public void PreviousPanel(string panel)
        {
            panelObserver.RenderringPanel(mapOfPanels.GetPanel(panel), false);

            if (SearchPreviousPanel(panel).Equals("MainPanel"))
                mainPanel.RefreshDisplay();
            panelObserver.RenderringPanel(mapOfPanels.GetPanel(SearchPreviousPanel(panel)), true);
        }
        */
        //TODO How to change between Panels, not linear.
        /*
        public void NextPanel(string panel) 
        {
            panelObserver.RenderringPanel(mapOfPanels.GetPanel(panel), false);
            panelObserver.RenderringPanel(mapOfPanels.GetPanel(SearchNextPanel(panel)), true);
        }
        */
        public void NextPanel(string nextPanelTag)
        {
            print(EventSystem.current.currentSelectedGameObject);
            print(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject);
            
            panelRenderer.RenderringPanel(EventSystem.current.currentSelectedGameObject.transform.root.gameObject, false);
            panelRenderer.RenderringPanel(GameObject.FindGameObjectWithTag(nextPanelTag), true);
        }
        /*
        private string SearchPreviousPanel(string actualPanel)
        {
            string result = "";
            for(int i= 0; i < ALLPANELS.Length; i++)
            {
                if (actualPanel.Equals(ALLPANELS[i]) && i - 1 >= 0)
                    result = ALLPANELS[i - 1];
            }
            Debug.Log(actualPanel);
            return result;
        }
        */
        /*
        private string SearchNextPanel(string actualPanel)
        {
            string result = "";
            for(int i = 0; i < ALLPANELS.Length; i++)
            {
                if (actualPanel.Equals(ALLPANELS[i]) && i + 1 < ALLPANELS.Length)
                    result = ALLPANELS[i + 1];
            }
            return result;
        }

        private bool ActivePanel(string panel)
        {
            bool exists = false;
            return exists;
        }
        */
    }
}
