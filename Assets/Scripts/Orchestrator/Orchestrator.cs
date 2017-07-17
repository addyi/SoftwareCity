using System.Collections.Generic;
using UnityEngine;
using Webservice;
using Webservice.Response.Authentication;
using Webservice.UriBuilding;
using Webservice.Response.Project;
using DataModel;
using DiskIO.AvailableMetrics;
using System;

namespace Orchestrator
{
    public class Orchestrator : MonoBehaviour
    {

        private string selectedProjectKey = "";
        private readonly Model model = Model.GetInstance();


        // Use this for initialization
        void Start()
        {
            model.SetAvailableMetrics(AvailableMetricConfigReader.ReadConfigFile());
            // TODO ADDYI load city from disk
        }

        public void GetLocalProject()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Load List of SonarQube Projects from the predefined Uri. 
        /// Depends on the Permissionlevel in SonarQube.
        /// </summary>
        /// <param name="callback">Function with List of Projects (can be empty) and long HTML Error Code.</param>
        public void LoadOnlineProjects(Action<List<SQProject>, long> callback)
        {
            SqProjectUriBuilder uriBuilder = new SqProjectUriBuilder(model.GetBaseUrl());
            if (model.GetUsername() != "" && model.GetPassword() != "")
                uriBuilder.UserCredentials(model.GetUsername(), model.GetPassword());

            StartCoroutine(WebInterface.WebRequest<List<SQProject>>(
               uriBuilder.GetSqUri(),
               (res, err) =>
               {
                   Debug.Log("LoadProjectList ResponseCode: " + err);
                   callback(res, err);                  
               }));
        }

        public List<Metric> GetAvailableMetrics()
        {
            return model.GetAvailableMetrics();
        }

        public void CredentialsValid(string baseUri, string username, string password, Action<bool, long> callback)
        {
            StartCoroutine(WebInterface.WebRequest<Auth>(
                new SqAuthValidationUriBuilder(baseUri, username, password).GetSqUri(),
                (res, err) =>
                {
                    if (err == 200)
                    {
                        model.SetCredentials(baseUri, username, password);
                        callback(res.valid, err);
                    }
                    callback(false, err);
                }));
        }

        public void SelectedMetrics(Metric[] SelectedMetrics)
        {
            model.SetSelectedMetrics(SelectedMetrics);
        }

        public void SecondMetricSelected()
        {
            throw new NotImplementedException();
        }

        public void ShowCity()
        {
            throw new NotImplementedException();
        }

        public void SelectedProject(string projectKey)
        {
            selectedProjectKey = projectKey;
        }

        private void LoadProject()
        {
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
    }
}