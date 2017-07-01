using UnityEngine;
using Webservice;
using Webservice.Response.Authentication;
using Webservice.UriBuilding;
using Webservice.Response.Project;
using Webservice.Response.ComponentTree;
using System.Collections.Generic;


//calls to generate a test enviroment
public class ButtonFunctions : MonoBehaviour
{
    [SerializeField]
    private GameObject enviroment;

    private bool enviromentExist;

    private void Start()
    {
        enviromentExist = false;

        const string baseUri = "http://sonarqube.eosn.de/";
        const string username = "user";
        const string pw = "123456";
        const string metricKeys = "ncloc,bugs,vulnerabilities,code_smells,violations,functions,coverage,test_success_density,comment_lines_density";
        const string projectKey = "geo-quiz-app";

        // TODO paging
        StartCoroutine(WebInterface.FuckingUnityRequest<Auth>(
            new SqAuthValidationUriBuilder(baseUri, username, pw).GetSqUri(),
            (res, err) =>
            {
                switch (err)
                {
                    case 200:
                        Debug.Log("Addyi Auth:" + res.valid.ToString());
                        break;
                    default:
                        Debug.Log("Addyi ResponseCode: " + err);
                        break;

                }
            }));

        StartCoroutine(WebInterface.FuckingUnityRequest<List<SQProject>>(
           new SqProjectUriBuilder(baseUri).UserCredentials(username, pw).GetSqUri(),
           (res, err) =>
           {
               switch (err)
               {
                   case 200:
                       res.ForEach((projekt) =>
                       {
                           Debug.Log("Addyi Projekt: " + projekt.ToString());
                       });
                       break;
                   default:
                       Debug.Log("Addyi ResponseCode: " + err);
                       break;

               }
           }));

        StartCoroutine(WebInterface.FuckingUnityRequest<ComponentTree>(
           new SqComponentTreeUriBuilder(baseUri, projectKey, metricKeys)
                .UserCredentials(username, pw)
           .GetSqUri(),
           (res, err) =>
           {
               switch (err)
               {
                   case 200:
                       Debug.Log(res.baseComponent.ToString());
                       Debug.Log(res.paging.ToString());
                       break;
                   default:
                       Debug.Log("Addyi ResponseCode: " + err);
                       break;

               }

           }));


    }

    public void CreateEnvironment()
    {
        if (!enviromentExist)
        {
            Instantiate(enviroment, transform.localPosition, transform.localRotation);
            enviromentExist = true;
        }

    }

}