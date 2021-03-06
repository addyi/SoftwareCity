﻿using ConfigurationWindow.ButtonEventHandling.WriteOnPanel;
using ConfigurationWindow.ConfigurationObserver;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ConfigurationWindow.ButtonEventHandling
{
    public class LoginPanelValidator : MonoBehaviour
    {

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
        /// A reference to textLabel, to show error message for the user.
        /// </summary>
        private GameObject errorCodeLabel;
        /// <summary>
        /// A reference to the LoginCredentials script, to get the last saved user datas.
        /// </summary>
        private LoginCredentials loginCredentials;

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
            errorCodeLabel = GameObject.FindGameObjectWithTag("ErrorCodeLabel");
            errorCodeLabel.GetComponent<Text>().color = Color.red;

            loginCredentials = GetComponent<LoginCredentials>();
            orchestrator = GameObject.FindGameObjectWithTag("Orchestrator");
            panelHandler = GameObject.FindGameObjectWithTag("PanelExchanger");
            inputField = GetComponentsInChildren<InputField>();
            checkButton = GameObject.FindGameObjectWithTag("CheckButton");

            urlInput = loginCredentials.GetLastURL();
            usernameInput = loginCredentials.GetLastUserName();

            foreach(InputField input in inputField)
            {
                input.onEndEdit.AddListener(delegate { CheckInput(input); });
            }

            checkButton.GetComponent<Button>().onClick.AddListener(ControlAuthentication);
        }

        /// <summary>
        /// Every time the login panel reloads the error message will be refreshed.
        /// </summary>
        public void RefreshDisplay()
        {
            errorCodeLabel.GetComponent<Text>().text = "";
        }

        /// <summary>
        /// Before the submit from the button the input data from the user will be verified.
        /// </summary>
        public void ControlAuthentication()
        {
            Debug.Log("Test");
            Debug.Log(urlInput);
            if (String.IsNullOrEmpty(urlInput))
                result = false;
            else
            {
                try
                {
                    orchestrator.GetComponent<Orchestrator.Orchestrator>().CredentialsValid(urlInput, usernameInput, passwordInput, (possible, err) =>
                       {
                           Debug.Log(err);
                           switch (err)
                           {
                               case 0:
                                   errorCodeLabel.GetComponent<Text>().text = "URI is invalid: " + urlInput;
                                   break;

                               case 200:
                                   if (possible)
                                   {
                                   //Alles ok User kann weiter machen
                                   Debug.Log(err);
                                       orchestrator.GetComponent<Orchestrator.Orchestrator>().LoadOnlineProjects((listOfProjects, error) =>
                                       {
                                           switch (error)
                                           {
                                               case 200:
                                               //Alles hat funktioniert
                                               //TODO Richard Liste der porjekte in eine andere liste einfügen und die buttons erstellen.
                                               Debug.Log("Kann Projekte laden Länge der liste: " + listOfProjects.Count);
                                                   RefreshDisplay();
                                                   panelHandler.GetComponent<ButtonPool>().AddButtons(listOfProjects);
                                                   break;
                                           }
                                       });
                                       panelHandler.GetComponent<PanelExchanger>().NextPanel("SamplePanel");
                                   }
                                   else
                                   {
                                   //Benutzername/Pw ist falsch.
                                   errorCodeLabel.GetComponent<Text>().text = "Benutzername/Password sind falsch";
                                   }
                                   break;
                               case 404:
                               //urlinput ist falsch
                               result = false;
                                   RefreshInputLabel(GameObject.FindGameObjectWithTag("URLInput").GetComponent<InputField>());
                                   errorCodeLabel.GetComponent<Text>().text = urlInput + " is false";
                                   break;
                               default:
                               //Fehlermeldung an User ist schief gelaufen
                               break;
                           }
                       });
                } catch(UriFormatException exception)
                {
                    errorCodeLabel.GetComponent<Text>().text = exception.Message;
                }
            }
        }

        /// <summary>
        /// Refreshes the display after every input from the user to inform the user, if he forgot some inputs.
        /// </summary>
        /// <param name="input">A reference to the Inputfield to show exactly where the user needs an input.</param>
        public void RefreshInputLabel(InputField input)
        {
            if(!result)
            {
                input.GetComponent<Image>().color = Color.red;
            } else
            {
                input.GetComponent<Image>().color = Color.white;
            }
        }

        /// <summary>
        /// Saves the input from the user in an string container.
        /// </summary>
        /// <param name="input">An reference to know which Inputfield is active.</param>
        void CheckInput(InputField input)
        {
            textLabel = input.text;
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
            RefreshInputLabel(input);
        }

        /// <summary>
        /// Verify the input from the user in the URLInput label and to check if the InputField is empty.
        /// </summary>
        /// <param name="input">The actually input from the user is saved in the string: urlinput.</param>
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

        /// <summary>
        /// Verify the input from the user, if this field is empty the usernameInput is empty.
        /// </summary>
        /// <param name="input">The actually input from the user is saved in the string: usernameInput.</param>
        private void CheckUserInput(string input)
        {
            if (String.IsNullOrEmpty(input))
                usernameInput = "";
            else
                usernameInput = input;
            result = true;
        }


        /// <summary>
        /// Verify the input from the user, this field can only be empty if the usernameInput is empty otherwise it informs the user.
        /// </summary>
        /// <param name="input">The actually input from the user is saved in the string: passwordInput.</param>
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
