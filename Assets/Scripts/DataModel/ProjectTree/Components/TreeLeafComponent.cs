using DataModel.Metrics;
using System;
using Webservice.Response.ComponentTree;

namespace DataModel.ProjectTree.Components
{
    /// <summary>
    /// Abstract tree leaf component definition
    /// </summary>
    [Serializable]
    public abstract class TreeLeafComponent : TreeComponent
    {
        /// <summary>
        /// Programming language the file is written in
        /// </summary>
        public string Language;

        /// <summary>
        /// Default constructor, needed because of the Serialization to disk
        /// </summary>
        public TreeLeafComponent() { }

        /// <summary>
        /// This is the constructor normaly used to instanciate a fully fledged obj
        /// </summary>
        /// <param name="component">SqComponent from the SQ API response</param>
        protected TreeLeafComponent(SqComponent component) : base(component)
        {
            Language = component.language;
        }

        /// <summary>
        /// Insert/Update a component at the specefied path
        /// </summary>
        /// <param name="path">path as string array e.g. ["src","some","path"]</param>
        /// <param name="component">Componet to insert</param>
        /// <returns>Returns the inserted component</returns>
        public override TreeComponent InsertComponentAt(string[] path, TreeComponent component)
        {
            if (component == null || path == null || path.Length <= 0)
                return null;
            if (path.Length == 0 && Name == component.Name)
                return UpdateComponent(component);
            return null;
        }

        /// <summary>
        /// Max value of the defined metic in whole tree
        /// </summary>
        /// <param name="MetricKey">sonarqube metric key</param>
        /// <returns>max value</returns>
        public override float GetMaxForMetric(string MetricKey)
        {
            foreach (TreeMetric tm in Metrics)
            {
                if (tm.Key == MetricKey)
                {
                    return tm.Value;
                }
            }
            return 0;
        }
    }
}
