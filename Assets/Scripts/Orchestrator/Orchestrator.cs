using System.Collections.Generic;
using UnityEngine;
using Webservice;
using Webservice.Response.Authentication;
using Webservice.UriBuilding;
using Webservice.Response.Project;
using DataModel;
using DiskIO.AvailableMetrics;
using System;
using Webservice.Response.ComponentTree;
using DataModel.ProjectTree.Components;

namespace Orchestrator
{
    public class Orchestrator : MonoBehaviour
    {

        private readonly Model model = Model.GetInstance();


        // Use this for initialization
        void Start()
        {
            model.SetAvailableMetrics(AvailableMetricConfigReader.ReadConfigFile());
            // TODO ADDYI load city from disk
            CredentialsValid("http://sonarqube.eosn.de/", "user", "123456", (success, code) =>
            {
                SelectProject("geo-quiz-app");
            });


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

        public void SelectMetrics(Metric[] SelectedMetrics)
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

        private bool IsLocalProjectRequestedProject(string projectKey)
        {
            ProjectComponent p = model.GetTree();
            if (p != null && p.Key == projectKey)
                return true;
            return false;
        }

        public void SelectProject(string projectKey)
        {
            if (!IsLocalProjectRequestedProject(projectKey))
            {
                LoadProject(projectKey, 1);
            }
        }

        private void LoadProject(string projectKey, int page)
        {
            SqComponentTreeUriBuilder uriBuilder
                = new SqComponentTreeUriBuilder(model.GetBaseUrl(), projectKey,
                model.GetAvailableMetricsAsString());

            if (model.GetUsername() != "" && model.GetPassword() != "")
            {
                uriBuilder.UserCredentials(model.GetUsername(), model.GetPassword());
            }

            StartCoroutine(WebInterface.WebRequest<ComponentTree>(
               uriBuilder.Page(page).GetSqUri(),
               (res, err) =>
               {
                   switch (err)
                   {
                       case 200:
                           model.BuildProjectTree(res.baseComponent, res.components);

                           int TotalNumOfPages = res.paging.total / res.paging.pageSize + 1;
                           if (page < TotalNumOfPages)
                           {
                               LoadProject(projectKey, page + 1);
                           }
                           break;
                       default:
                           Debug.Log("Addyi ResponseCode: " + err);
                           break;
                   }
               }));
        }


    }
}