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

        private InputField[] inputField;
        private GameObject checkButton;

        private GameObject orchestrator;
        private GameObject panelHandler;


        private string urlInput;
        private string usernameInput;
        private string passwordInput;

        private Uri resultLink;
        private string textLabel;
        private readonly string URI;
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
