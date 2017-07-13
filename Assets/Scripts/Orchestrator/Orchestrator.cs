using System.Collections.Generic;
using UnityEngine;
using Webservice;
using Webservice.Response.Authentication;
using Webservice.UriBuilding;
using Webservice.Response.Project;
using Webservice.Response.ComponentTree;
using DataModel;
using DataModel.ProjectTree;
using DiskIO.AvailableMetrics;
using System;

namespace Orchestrator
{
    public class Orchestrator : MonoBehaviour
    {
        private string baseUri = "";
        private string username = "";
        private string password = "";


        // Use this for initialization
        void Start()
        {
            //const string metricKeys = "ncloc,bugs,vulnerabilities,code_smells,violations,functions,coverage,test_success_density,comment_lines_density";
            //const string projectKey = "geo-quiz-app";

            //// TODO ADDYI REMOVE DEBUG LOGS
            //StartCoroutine(WebInterface.WebRequest<Auth>(
            //    new SqAuthValidationUriBuilder(baseUri, username, password).GetSqUri(),
            //    (res, err) =>
            //    {
            //        switch (err)
            //        {
            //            case 200:
            //                Debug.Log("Addyi Auth:" + res.valid.ToString());
            //                break;
            //            default:
            //                Debug.Log("Addyi ResponseCode: " + err);
            //                break;

            //        }
            //    }));

            //StartCoroutine(WebInterface.WebRequest<List<SQProject>>(
            //   new SqProjectUriBuilder(baseUri).UserCredentials(username, password).GetSqUri(),
            //   (res, err) =>
            //   {
            //       switch (err)
            //       {
            //           case 200:
            //               res.ForEach((projekt) =>
            //               {
            //                   Debug.Log("Addyi Projekt: " + projekt.ToString());
            //               });
            //               break;
            //           default:
            //               Debug.Log("Addyi ResponseCode: " + err);
            //               break;

            //       }
            //   }));

            //StartCoroutine(WebInterface.WebRequest<ComponentTree>(
            //   new SqComponentTreeUriBuilder(baseUri, projectKey, metricKeys)
            //        .UserCredentials(username, password).GetSqUri(),
            //   (System.Action<ComponentTree, long>)((res, err) =>
            //   {
            //       switch (err)
            //       {
            //           case 200:
            //               Debug.Log(res.baseComponent.ToString());
            //               Debug.Log(res.paging.ToString());
            //               IProjectTree ProjectTree = new Model();

            //               // List<Webservice.Response.ComponentTree.Component> components = res.components;
            //               //components.Sort();


            //               ProjectTree.BuildProjectTree(res.baseComponent, res.components);
            //               Debug.Log(ProjectTree.GetTree().ToString());

            //               break;
            //           default:
            //               Debug.Log("Addyi ResponseCode: " + err);
            //               break;

            //       }

            //   })));


        }

        public void GetLocalProject()
        {
            throw new NotImplementedException();
        }

        public void GetAvailableProjects()
        {
            throw new NotImplementedException();
        }

        public List<Metric> GetAvailableMetrics()
        {
            return AvailableMetricConfigReader.ReadConfigFile();
        }

        public void CredentialsValid(string baseUri, string username, string password, Action<bool, long> callback)
        {
            StartCoroutine(WebInterface.WebRequest<Auth>(
                new SqAuthValidationUriBuilder(baseUri, username, password).GetSqUri(),
                (res, err) =>
                {
                    if (err == 200)
                    {
                        this.baseUri = baseUri;
                        this.username = username;
                        this.password = password;
                        callback(res.valid, err);
                    }
                    callback(false, err);
                }));
        }

        public void SelectedMetrics(string[] KeysSelectedMetrics)
        {
            throw new NotImplementedException();
        }

        public void SecondMetricSelected()
        {
            throw new NotImplementedException();
        }

        public void ShowCity()
        {
            throw new NotImplementedException();
        }
    }
}