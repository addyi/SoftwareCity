using ConfigurationWindow.ButtonEventHandling.WriteOnPanel;
using ConfigurationWindow.ConfigurationObserver;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ConfigurationWindow.ButtonEventHandling
{
    public class LoginPanelValidator : MonoBehaviour
    {
        //public InputField uriTextInput;

        /// <summary>
        /// A reference to all inputfields, to add listener to them.
        /// </summary>
        private InputField[] inputField;
        /// <summary>
        /// A reference to the checkButton to add specific listener.
        /// </summary>
        private GameObject checkButton;

        /// <summary>
        /// A reference to the Orchestrator script, to make the call to the server.
        /// </summary>
        private GameObject orchestrator;
        /// <summary>
        /// A reference to the PanelExchanger script, to change between panels.
        /// </summary>
        private GameObject panelHandler;

        /// <summary>
        /// urlInput saves the input from the textlabel URI.
        /// </summary>
        private string urlInput;
        /// <summary>
        /// usernameInput saves the input from the textlabel Username.
        /// </summary>
        private string usernameInput;
        /// <summary>
        /// passwordInput saves the input from the textlabe Password.
        /// </summary>
        private string passwordInput;

        /// <summary>
        /// Template container to get the input from the inputlabel.
        /// </summary>
        private string textLabel;
        /// <summary>
        /// Checks the input if there is some signs in the inputlabel.
        /// </summary>
        private bool result;

        private void Start()
        {
            orchestrator = GameObject.FindGameObjectWithTag("Orchestrator");
            panelHandler = GameObject.FindGameObjectWithTag("PanelExchanger");
            inputField = GetComponentsInChildren<InputField>();
            checkButton = GameObject.FindGameObjectWithTag("CheckButton");
            foreach(InputField input in inputField)
            {
                input.onEndEdit.AddListener(delegate { CheckInput(input); });
            }

            checkButton.GetComponent<Button>().onClick.AddListener(ControlAuthentication);
        }

        public void ControlAuthentication()
        {
            Debug.Log("Test");
            if (String.IsNullOrEmpty(urlInput))
                result = false;
            else
            {
                orchestrator.GetComponent<Orchestrator.Orchestrator>().CredentialsValid(urlInput, usernameInput, passwordInput, (possible, err) =>
                   {
                       Debug.Log(err);
                       switch(err)
                       {
                           case 200:
                               if(possible)
                               {
                                   //Alles ok User kann weiter machen
                                   Debug.Log(err);
                                   orchestrator.GetComponent<Orchestrator.Orchestrator>().LoadOnlineProjects((listOfProjects, error) =>
                                   {
                                       switch(error)
                                       {
                                           case 200:
                                               //Alles hat funktioniert
                                               //TODO Richard Liste der porjekte in eine andere liste einfügen und die buttons erstellen.
                                               Debug.Log("Kann Projekte laden Länge der liste: " + listOfProjects.Count);
                                               panelHandler.GetComponent<ButtonPool>().AddButtons(listOfProjects);
                                               break;
                                       }
                                   });
                                   panelHandler.GetComponent<PanelExchanger>().NextPanel("SamplePanel");
                               }
                               else
                               {
                                   //Benutzername/Pw ist falsch.
                               }
                               break;
                           case 404:
                               //urlinput ist falsch
                               result = false;
                               RefreshDisplay(GameObject.FindGameObjectWithTag("URLInput").GetComponent<InputField>());
                               break;
                           default:
                               //Fehlermeldung an User ist schief gelaufen
                               break;
                       }
                   });
            }
        }

        public void RefreshDisplay(InputField input)
        {
            Debug.Log(urlInput);
            if(!result)
            {
                input.GetComponent<Image>().color = Color.red;
            } else
            {
                input.GetComponent<Image>().color = Color.white;
            }
        }

        void CheckInput(InputField input)
        {
            textLabel = input.text;
            Debug.Log(textLabel);
            switch(input.tag)
            {
                case "URLInput":
                    Debug.Log("Url input läuft");
                    CheckURLInput(textLabel);
                    break;
                case "UserInput":
                    CheckUserInput(textLabel);
                    break;
                case "PasswordInput":
                    CheckPasswordInput(textLabel);
                    break;
            }
            RefreshDisplay(input);
        }

        private void CheckURLInput(string input)
        {
            if (String.IsNullOrEmpty(input))
                result = false;
            else
            {
                result = true;
                urlInput = input;
            }
        }

        private void CheckUserInput(string input)
        {
            Debug.Log(input);
            if (String.IsNullOrEmpty(input))
                usernameInput = "";
            else
                usernameInput = input;
            result = true;
        }

        private void CheckPasswordInput(string input)
        {

            if (String.IsNullOrEmpty(usernameInput) && String.IsNullOrEmpty(input))
                result = false;
            else
            {
                result = true;
                passwordInput = input;
            }
        }
    }
}
