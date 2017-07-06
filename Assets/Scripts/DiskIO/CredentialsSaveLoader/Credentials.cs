using System;
using UnityEngine;
using System.Collections;

namespace DiskIO.CredentialsSaveLoader
{
    public class Credentials
    {
        public Credentials(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public void SaveCredentials()
        {
            PlayerPrefs.SetString("Username", this.Username);
            PlayerPrefs.SetString("Password", this.Password);
        }

        public string LoadUsername(){
            return PlayerPrefs.GetString("Username");
        }

		public string LoadPassword()
		{
			return PlayerPrefs.GetString("Password");
		}
    }
}
