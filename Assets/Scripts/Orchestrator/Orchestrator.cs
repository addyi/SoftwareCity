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
    /// <summary>
    /// The CityLoadingState is used to monitor if the city is loaded and stored correctly in the model.
    /// </summary>
    public enum CityLoadingState
    {
        /// <summary>
        /// The City is not ready
        /// </summary>
        NotReady,
        /// <summary>
        /// The City is ready and stored in the model 
        /// </summary>
        Ready,
        /// <summary>
        /// Some Error occurred and the city is not ready
        /// </summary>
        LodingError
    }

    /// <summary>
    /// Central orchestrator of the program
    /// </summary>
    public class Orchestrator : MonoBehaviour
    {
        /// <summary>
        /// The Model instance for the orchestrator
        /// </summary>
        private readonly Model model = Model.GetInstance();
        /// <summary>
        /// The CityLoadingState is used to monitor if the city is loaded and stored correctly in the model.
        /// </summary>
        private static CityLoadingState cityLoadingState = CityLoadingState.NotReady;
        /// <summary>
        /// temporary stored projectKey the orchestrator needs to load after the second metric is selected
        /// </summary>
        private string toLoadProjectKey = "";
        /// <summary>
        /// The Reference to the rendered city
        /// </summary>
        [SerializeField]
        private GameObject enviroment;
        /// <summary>
        /// guard that it is only possible to place one city
        /// </summary>
        private bool enviromentExist = false;

        /// <summary>
        /// Unity calls this at initialization of the orchestrator game object
        /// </summary>
        void Start()
        {
            // Read availablemetrics from disk and store them in the model
            model.SetAvailableMetrics(AvailableMetricConfigReader.ReadConfigFile());
            enviromentExist = false;

            LoadLocalProject();
        }

        /// <summary>
        /// Returns the project in the model e.g. to get the project metrics in the config window
        /// </summary>
        /// <returns>Stored project tree from the model</returns>
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

        /// <summary>
        /// Returns list of AvailableMetrics
        /// </summary>
        /// <returns>List of AvailableMetrics</returns>
        public List<Metric> GetAvailableMetrics()
        {
            return model.GetAvailableMetrics();
        }

        /// <summary>
        /// Validates the credentials. The SonarQube API Returns true if no username and password are set.
        /// </summary>
        /// <param name="baseUri">Url to the SonarQube Server e.g. "http://sonarqube.test.de/"</param>
        /// <param name="username">The Username for the SonarQube API</param>
        /// <param name="password">The Password for the SonarQube API</param>
        /// <param name="callback">Callback Action with bool and long. If the bool is true the credentials are valid. 
        /// Long is the html statuscode or -1 if some other Exception occurred.</param>
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

        /// <summary>
        /// Sets the user selected metrics. 
        /// Array of the Selected Metrics for the Rendering of the building dimensions.
        /// 0. Area 1. Height 2. Color 4. Pyramid
        /// </summary>
        /// <param name="SelectedMetrics">Array of the Selected Metrics</param>
        public void SelectMetrics(Metric[] SelectedMetrics)
        {
            model.SetSelectedMetrics(SelectedMetrics);
        }

        /// <summary>
        /// If the second metric is selected this method should be called. This starts the request for the city to load.
        /// </summary>
        public void SecondMetricSelected()
        {
            // The CityLoadingState is Ready if the local project is requested
            // If the baseUrl is set the credentials are valid
            if (cityLoadingState != CityLoadingState.Ready
                && model.GetBaseUrl() != null)
            {
                model.DeleteTree();
                LoadOnlineProject(toLoadProjectKey, 1);
                cityLoadingState = CityLoadingState.Ready;
            }
        }

        /// <summary>
        /// Returns the city loading state
        /// </summary>
        /// <returns>city loading state</returns>
        public CityLoadingState IsCityReady()
        {
            return cityLoadingState;
        }

        /// <summary>
        /// Render the City
        /// </summary>
        public void ShowCity()
        {
            if (IsCityReady() == CityLoadingState.Ready && !enviromentExist)
            {
                Instantiate(enviroment, transform.localPosition, transform.localRotation);
                enviromentExist = true;
            }
        }

        /// <summary>
        /// Select the local project as to render
        /// </summary>
        /// <param name="projectKey">SonarQube Project Key</param>
        /// <exception cref="ArgumentException">If the key does't match the local project</exception>
        public void SelectLocalProject(string projectKey)
        {
            ProjectComponent p = model.GetTree();
            if (p == null || p.Key != projectKey)
            {
                throw new ArgumentException("The requested project is not locally available");
            }
            cityLoadingState = CityLoadingState.Ready;
            toLoadProjectKey = projectKey;
        }

        /// <summary>
        /// Sets the project key for the online project. 
        /// The request for the project is done in SecondMetricSelected() method, 
        /// therefore no checks are done and exceptions are thrown. 
        /// If an exception occures the city loading state is set to LodingError.
        /// </summary>
        /// <param name="projectKey">SonarQube Project Key</param>
        public void SelectOnlineProject(string projectKey)
        {
            toLoadProjectKey = projectKey;
        }

        /// <summary>
        /// Requests all project components and their metrics from the sonarqube api
        /// </summary>
        /// <param name="projectKey">SonarQube Project Key</param>
        /// <param name="page">The page of the request, should be 1. 
        /// The other calls are recursive in the method.</param>
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
                               if (cityLoadingState != CityLoadingState.LodingError)
                               {
                                   cityLoadingState = CityLoadingState.Ready;
                               }
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

        /// <summary>
        /// Load local stored project tree
        /// </summary>
        private void LoadLocalProject()
        {
            try
            {
                ProjectComponent tree = ComponentTreeStream.LoadProjectComponent();
                model.SetTree(tree);
            }
            catch (Exception e)
            {
                Debug.Log("Not able to load Project from disk: " + e.Message);
                model.SetTree(null);
            }
        }

        /// <summary>
        /// Removes the rendered city
        /// </summary>
        public void DestroyEnviroment()
        {
            Destroy(GameObject.FindGameObjectWithTag("Enviroment"));
            enviromentExist = false;
            toLoadProjectKey = "";
            cityLoadingState = CityLoadingState.NotReady;
            LoadLocalProject();
        }
    }
}