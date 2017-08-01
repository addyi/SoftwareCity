using UnityEngine;
using UnityEngine.UI;

namespace ConfigurationWindow
{
    public class LoginCredentials : MonoBehaviour {

        /// <summary>
        /// Reference to the url text gameobject.
        /// </summary>
        [SerializeField]
        private Text urlText;

        [SerializeField]
        private InputField urlInputField;

        /// <summary>
        /// Key to get value url from PlayerPrefs.
        /// </summary>
        public static readonly string urlKey = "urlKey";

        /// <summary>
        /// Reference to the username text gameobject.
        /// </summary>
        [SerializeField]
        private Text usernameText;

        [SerializeField]
        private InputField usernameInputField;

        /// <summary>
        /// Key to get value username from PlayerPrefs.
        /// </summary>
        public static readonly string usernameKey = "usernameKey";
    
        /// <summary>
        /// Set saved credentials.
        /// </summary>
	    void Start () {
            if (PlayerPrefs.HasKey(usernameKey))
                usernameInputField.text = PlayerPrefs.GetString(usernameKey);

            if (PlayerPrefs.HasKey(urlKey))
                urlInputField.text = PlayerPrefs.GetString(urlKey);
        }

        /// <summary>
        /// Get the saved URL from the user input.
        /// </summary>
        /// <returns>Returns an url as a string.</returns>
        public string GetLastURL()
        {
            return PlayerPrefs.GetString(urlKey);
        }
        /// <summary>
        /// Get the last saved username from the user input.
        /// </summary>
        /// <returns>Returns an username as a string.</returns>
        public string GetLastUserName()
        {
            return PlayerPrefs.GetString(usernameKey);
        }

        public void SaveCredentials()
        {
            if (PlayerPrefs.HasKey(usernameKey))
                PlayerPrefs.DeleteKey(usernameKey);
            PlayerPrefs.SetString(usernameKey, usernameText.text);

            if (PlayerPrefs.HasKey(urlKey))
                PlayerPrefs.DeleteKey(urlKey);
            PlayerPrefs.SetString(urlKey, urlText.text);
        }
    }
}

