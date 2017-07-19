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
        private GameObject passwordInput;


        private Uri resultLink;
        private string textLabel;
        private readonly string URI;
        private bool result;

        private void Start()
        {
            inputField = GetComponentsInChildren<InputField>();
            foreach(InputField input in inputField)
            {
                input.onEndEdit.AddListener(delegate { CheckInput(input); });
            }
            //uriTextInput.onEndEdit.AddListener(delegate { CheckInput(uriTextInput); });
        }

        void RefreshDisplay(InputField input)
        {
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
            //Debug.Log(textLabel);
            if (input.tag.Equals("URLInput"))
                Debug.Log("Url input: " + textLabel);
            result = Uri.TryCreate(textLabel, UriKind.Absolute, out resultLink)
                && (resultLink.Scheme == Uri.UriSchemeHttp || resultLink.Scheme == Uri.UriSchemeHttps);
            if(result)
                Debug.Log(resultLink.Scheme);
            RefreshDisplay(input);
        }
    }
}
