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
        public InputField uriTextInput;


        private Uri resultLink;
        private string textLabel;
        private readonly string URI;
        private bool result;

        private void Start()
        {
            uriTextInput.onEndEdit.AddListener(delegate { CheckInput(uriTextInput); });
        }

        void RefreshDisplay()
        {
            if(!result)
            {
                uriTextInput.GetComponent<Image>().color = Color.red;
            } else
            {
                uriTextInput.GetComponent<Image>().color = Color.white;
            }
        }

        void CheckInput(InputField input)
        {

            textLabel = input.text;
            result = Uri.TryCreate(textLabel, UriKind.Absolute, out resultLink)
                && (resultLink.Scheme == Uri.UriSchemeHttp || resultLink.Scheme == Uri.UriSchemeHttps);
            if(result)
                Debug.Log(resultLink.Scheme);
            RefreshDisplay();
        }
    }
}
