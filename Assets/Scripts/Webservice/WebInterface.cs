using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace Webservice
{
    /// <summary>
    /// This is the class responsible for requesting data from the SonarQube API
    /// </summary>
    public static class WebInterface
    {
        /// <summary>
        /// Request JSON Data from the SonarQube API for a specific Uri and deserializing it to C# objects
        /// </summary>
        /// <typeparam name="T">Deserializing Type</typeparam>
        /// <param name="uri">Uri for the request</param>
        /// <param name="callback">Async response methode. It has the parameters T and long. 
        /// Long is the html statuscode or -1 if some other Exception occurred.</param>
        /// <returns>IEnumerator is some foo for the unity StartCoroutine() method</returns>
        public static IEnumerator WebRequest<T>(Uri uri, Action<T, long> callback)
        {
            UnityWebRequest www = UnityWebRequest.Get(uri.ToString());
            yield return www.Send();

            if (www.isError)
            {
                // If the API sends a error html statuscode
                callback(default(T), www.responseCode);
            }
            else
            {
                // Try to deserialize the json response
                Debug.Log("Addyi Response: " + www.downloadHandler.text);
                try
                {
                    string json = www.downloadHandler.text;

                    // the fucking unity JSON deserializer is not able to handle arrays 
                    // as response, so i need to hack an object around it.
                    // To get this Object T must be ArrayResponseSQProject class
                    if (json.StartsWith("["))
                    {
                        json = "{\"array\":" + json + "}";
                    }

                    T resObj = JsonUtility.FromJson<T>(json);
                    callback(resObj, www.responseCode);
                }
                catch (Exception e)
                {
                    Debug.LogWarning("Addyi Error: " + e.ToString());
                    callback(default(T), -1);
                }
            }
        }
    }
}