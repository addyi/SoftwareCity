using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Webservice.Response.ArrayResponseSQProject;
using Webservice.Response.Project;

namespace Webservice
{
    public static class WebInterface
    {
        public static IEnumerator WebRequest<T>(Uri uri, Action<T, long> callback)
        {
            UnityWebRequest www = UnityWebRequest.Get(uri.ToString());
            yield return www.Send();

            if (www.isError)
            {
                callback(default(T), www.responseCode);
            }
            else
            {
                Debug.Log("Addyi Response: " + www.downloadHandler.text);
                try
                {
                    // handling if only JSON array as response is available -> [...]
                    if (www.downloadHandler.text.StartsWith("["))
                    {
                        string json = www.downloadHandler.text;
                        json = "{\"array\":" + json + "}";
                        T resArrObj = JsonUtility.FromJson<T>(json);
                        callback(resArrObj, www.responseCode);
                    }
                    else
                    {
                        T resObj = JsonUtility.FromJson<T>(www.downloadHandler.text);
                        Debug.Log(resObj);
                        callback(resObj, www.responseCode);
                    }

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