using System;
using System.Collections.Generic;
using DataModel.ProjectTree.Components;
using DataModel.ProjectTree;
using Webservice.Response.ComponentTree;
using DataModel.Metrics;
using DiskIO.AvailableMetrics;
using DataModel.UserData;

namespace DataModel
{
    /// <summary>
    /// Qalifier for the Tree Component. Equvalent to SQ Responce.
    /// </summary>
    public enum SqQualifier
    {
        /// <summary>
        /// If a folder is not returned by the API Call, but is needed for the Tree structure.
        /// </summary>
        UNDEFINED,
        SUB_PROJECT,
        DIRECTORY,
        FILE,
        PROJECT,
        UNIT_TEST
    }

    /// <summary>
    /// Central Data dump
    /// </summary>
    public class Model : IProjectTree, IAvailableMetrics, ISelectedMetrics, IUserData
    {
        /// <summary>
        /// Singleton model instance
        /// </summary>
        private readonly static Model model = new Model();
        /// <summary>
        /// Root of the project component tree
        /// </summary>
        private ProjectComponent project;
        /// <summary>
        /// Array of the Selected Metrics for the Rendering of the building dimensions
        /// 0. Area 1. Height 2. Color 4. Pyramid
        /// </summary>
        private Metric[] SelectedMetrics;
        /// <summary>
        /// Temporary Storage of UserCredentials for the Request
        /// </summary>
        private readonly UserCredentials userCredentials = new UserCredentials();
        /// <summary>
        /// List of the available metrics. All these metrics will be requested.
        /// The metrics are defined in a config file and will be read at program start.
        /// </summary>
        private List<Metric> availableMetrics = new List<Metric>();

        /// <summary>
        /// Private constructor because of the singleton pattern
        /// </summary>
        private Model() { }

        /// <summary>
        /// Returns the Instance of the singleton pattern
        /// </summary>
        /// <returns>Singleton Model instance</returns>
        public static Model GetInstance() { return model; }

        /// <summary>
        /// Constructs the component tree of the baseComponent and a list of components.
        /// </summary>
        /// <param name="baseComponent">Project Infos & Metrics</param>
        /// <param name="components">List of Components</param>
        /// <returns>Root of ComponentTree</returns>
        public ProjectComponent BuildProjectTree(SqComponent baseComponent,
            List<SqComponent> components)
        {
            if (baseComponent == null || components == null)
                return null;

            if (project == null)
                project = new ProjectComponent(baseComponent);

            lock (project)
            {
                foreach (SqComponent c in components)
                {
                    string[] s = c.path.Split('/');
                    project.InsertComponentAt(s, GetTreeComponent(c));
                }
            }
            return project;
        }

        /// <summary>
        /// Returns the list of available metrics
        /// </summary>
        /// <returns>List of available metrics</returns>
        public List<Metric> GetAvailableMetrics()
        {
            return availableMetrics;
        }

        /// <summary>
        /// Returns a dictionary of available metrics with the metric key as key
        /// </summary>
        /// <returns>Dictionary of available metrics</returns>
        public Dictionary<string, Metric> GetAvailableMetricsAsDictionary()
        {
            List<Metric> metrics = GetAvailableMetrics();
            Dictionary<string, Metric> dictionary = new Dictionary<string, Metric>();
            foreach (Metric m in metrics)
            {
                dictionary.Add(m.key, m);
            }
            return dictionary;
        }

        /// <summary>
        /// Returns the list of available metrics as a single string.
        /// This is used for the API request.
        /// </summary>
        /// <returns>List of available metrics as a single string</returns>
        public string GetAvailableMetricsAsString()
        {
            List<Metric> metrics = GetAvailableMetrics();
            string res = "";

            if (metrics == null || metrics.Count <= 0)
                return res;

            foreach (Metric m in metrics)
            {
                res += m.key + ",";
            }

            return res.Substring(0, res.Length - 1);
        }

        /// <summary>
        /// Returns the base url of the sonarqube server
        /// </summary>
        /// <returns>base url of the sonarqube server</returns>
        public string GetBaseUrl()
        {
            return userCredentials.baseUri;
        }

        /// <summary>
        /// Returns the password of the user for the sonarqube server
        /// </summary>
        /// <returns>password of the user for the sonarqube server</returns>
        public string GetPassword()
        {
            return userCredentials.password;
        }

        /// <summary>
        /// Returns the selected metrics array
        /// </summary>
        /// <returns>Selected metrics array</returns>
        public Metric[] GetSelectedMetrics()
        {
            return SelectedMetrics;
        }

        /// <summary>
        /// Returns the root of the project tree
        /// </summary>
        /// <returns>root of the project tree</returns>
        public ProjectComponent GetTree() { return project; }

        /// <summary>
        /// Sets the root of the project tree e.g. if a tree is read from disk
        /// </summary>
        /// <param name="tree">Root of the project tree</param>
        public void SetTree(ProjectComponent tree) { project = tree; }

        /// <summary>
        /// Sets the tree to null
        /// </summary>
        public void DeleteTree() { project = null; }

        /// <summary>
        /// Returns the username for the sonarqube server
        /// </summary>
        /// <returns>username for the sonarqube server</returns>
        public string GetUsername()
        {
            return userCredentials.username;
        }

        /// <summary>
        /// Sets the AvailableMetrics list e.g. if the 
        /// list is read from the config file
        /// </summary>
        /// <param name="AvailableMetrics">AvailableMetrics List</param>
        public void SetAvailableMetrics(List<Metric> AvailableMetrics)
        {
            this.availableMetrics = AvailableMetrics;
        }

        /// <summary>
        /// Sets the UserCredentials and BaseUrl for the SonarQube server
        /// </summary>
        /// <param name="BaseUrl">BaseUrl for the SonarQube server</param>
        /// <param name="Username">Username for the SonarQube server</param>
        /// <param name="Password">Password for the SonarQube server</param>
        public void SetCredentials(string BaseUrl, string Username, string Password)
        {
            userCredentials.baseUri = BaseUrl;
            userCredentials.username = Username;
            userCredentials.password = Password;
        }

        /// <summary>
        /// Sets the metrics the user has selected
        /// </summary>
        /// <param name="SelectedMetrics">Metrics the user has selected</param>
        public void SetSelectedMetrics(Metric[] SelectedMetrics)
        {
            this.SelectedMetrics = SelectedMetrics;
        }

        /// <summary>
        /// Returns the correct object representation for the specific 
        /// SqComponent based on the qualifier. These qualifier come from the SQ API.
        /// </summary>
        /// <param name="component">SqComponent for which a TreeComponent is needed</param>
        /// <returns>TreeComponent for the SqComponent based on the qualifier</returns>
        private TreeComponent GetTreeComponent(SqComponent component)
        {
            // Tests (UTS) and Sub-Projects (BRC) are not welcome in the tree
            // The Project Component (TRK) will not be created by this and is in the BaseComponent
            switch (component.qualifier)
            {
                case "BRC": return null;
                case "DIR": return new DirComponent(component);
                case "FIL": return new FilComponent(component);
                case "TRK": return null;
                case "UTS": return null;
                default: throw new ArgumentException("Unknown Qualifier: \"" + component.qualifier + "\"");
            }
        }
    }
}
