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

    class Model : IProjectTree, IAvailableMetrics, ISelectedMetrics, IUserData
    {
        private readonly static Model model = new Model();
        private ProjectComponent project;
        private Metric[] SelectedMetrics;
        private readonly UserCredentials userCredentials = new UserCredentials();
        private List<Metric> availableMetrics = new List<Metric>();

        private Model() { }

        public static Model GetInstance() { return model; }

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

        public List<Metric> GetAvailableMetrics()
        {
            return availableMetrics;
        }

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

        public string GetBaseUrl()
        {
            return userCredentials.baseUri;
        }

        public string GetPassword()
        {
            return userCredentials.password;
        }

        public Metric[] GetSelectedMetrics()
        {
            return SelectedMetrics;
        }

        public ProjectComponent GetTree() { return project; }

        public void SetTree(ProjectComponent tree) { project = tree; }

        public void DeleteTree() { project = null; }

        public string GetUsername()
        {
            return userCredentials.username;
        }

        public void SetAvailableMetrics(List<Metric> AvailableMetrics)
        {
            this.availableMetrics = AvailableMetrics;
        }

        public void SetCredentials(string BaseUrl, string Username, string Password)
        {
            userCredentials.baseUri = BaseUrl;
            userCredentials.username = Username;
            userCredentials.password = Password;
        }

        public void SetSelectedMetrics(Metric[] SelectedMetrics)
        {
            this.SelectedMetrics = SelectedMetrics;
        }

        private TreeComponent GetTreeComponent(SqComponent component)
        {
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
