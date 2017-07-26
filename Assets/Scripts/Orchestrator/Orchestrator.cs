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
using DiskIO.ProjectTreeSaveLoader;
using Webservice.Response.ArrayResponseSQProject;

namespace Orchestrator
{
    public enum CityLoadingState
    {
        NotReady,
        Ready,
        LodingError
    }

    public class Orchestrator : MonoBehaviour
    {

        private readonly Model model = Model.GetInstance();
        private CityLoadingState cityLoadingState = CityLoadingState.NotReady;
        private string toLoadProjectKey = "";
        [SerializeField]
        private GameObject enviroment;
        private bool enviromentExist;

        // Use this for initialization
        void Start()
        {
            // Read availablemetrics from disk and store them in the model
            model.SetAvailableMetrics(AvailableMetricConfigReader.ReadConfigFile());
            enviromentExist = false;

            LoadLocalProject();


            //CredentialsValid("http://sonarqube.eosn.de/", "user", "123456", (success, code) =>
            //{
            //    if (code == 200)
            //    {
            //        SelectProject("geo-quiz-app");
            //        SecondMetricSelected();
            //        Debug.Log(IsCityReady());
            //    }
            //});



        }

        public ProjectComponent GetLocalProject()
        {
            return model.GetTree();
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

            StartCoroutine(WebInterface.WebRequest<ArrayResponseSQProject>(
               uriBuilder.GetSqUri(),
               (res, err) =>
               {
                   Debug.Log("LoadProjectList ResponseCode: " + err);
                   callback(new List<SQProject>(res.array), err);
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
            if (!IsLocalProjectRequestedProject(toLoadProjectKey))
            {
                // If the url is set the credentials are valid
                if (model.GetBaseUrl() != null)
                {
                    model.DeleteTree();
                    LoadOnlineProject(toLoadProjectKey, 1);
                }
            }
            else
            {
                cityLoadingState = CityLoadingState.Ready;
            }
        }

        public CityLoadingState IsCityReady()
        {
            return cityLoadingState;
        }

        public void ShowCity()
        {
            //throw new NotImplementedException();

            //if (IsCityReady() == CityLoadingState.Ready)
            //{
                // TODO TOBIAS render city
                if (!enviromentExist)
                {
                    Instantiate(enviroment, transform.localPosition, transform.localRotation);
                    enviromentExist = true;
                }



            //}
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
            toLoadProjectKey = projectKey;
        }

        private void LoadOnlineProject(string projectKey, int page)
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
                               LoadOnlineProject(projectKey, page + 1);
                           }
                           else
                           {
                               if (cityLoadingState != CityLoadingState.LodingError) { cityLoadingState = CityLoadingState.Ready; }
                               ComponentTreeStream.SaveProjectComponent(model.GetTree());
                           }
                           break;
                       default:
                           Debug.Log("Addyi ResponseCode: " + err);
                           cityLoadingState = CityLoadingState.LodingError;
                           break;
                   }
               }));
        }

        private void LoadLocalProject()
        {
            try
            {
                ProjectComponent tree = ComponentTreeStream.LoadProjectComponent();
                model.SetTree(tree);
                Debug.Log("Loaded Project from Disk");
            }
            catch (Exception e)
            {
                Debug.Log("Not able to load Project from disk: " + e.Message);
                model.SetTree(null);
            }
        }
    }
}