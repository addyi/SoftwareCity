using System;
using System.Collections.Generic;
using System.Linq;
using DataModel.Metrics;
using Webservice.Response.ComponentTree;
using UnityEngine;

namespace DataModel.ProjectTree.Components
{
    /// <summary>
    /// Abstract tree component definition
    /// </summary>
    [Serializable]
    public abstract class TreeComponent
    {
        /// <summary>
        /// Component id (sonarqube)
        /// </summary>
        public string ID;
        /// <summary>
        /// component key (sonarqube)
        /// </summary>
        public string Key;
        /// <summary>
        /// component name
        /// </summary>
        public string Name;
        /// <summary>
        /// component name (file name or dir name) NOT the whole path
        /// </summary>
        public string Path;
        /// <summary>
        /// component qualifier equivalent to SQ 
        /// (see <see cref="DataModel.SqQualifier"/>)
        /// </summary>
        public SqQualifier Qualifier;
        /// <summary>
        /// List of metrics for this component
        /// </summary>
        public List<TreeMetric> Metrics = new List<TreeMetric>();

        /// <summary>
        /// Default constructor, needed because of the Serialization to disk
        /// </summary>
        public TreeComponent() { }

        /// <summary>
        /// This is the constructor normaly used to instanciate a fully fledged obj
        /// </summary>
        /// <param name="component">SqComponent from the SQ API response</param>
        protected TreeComponent(SqComponent component)
        {
            ID = component.id;
            Key = component.key;
            Name = component.name.Split('/').Last();
            Path = component.path;
            Qualifier = QualifierForString(component.qualifier);
            Metrics = TransformToTreeMetrics(component.measures);
        }

        /// <summary>
        /// This is the constructor if a folder is needed, but no SqComponent is available
        /// </summary>
        /// <param name="Name">name for the component</param>
        protected TreeComponent(string Name) { this.Name = Name; }

        /// <summary>
        /// Insert/Update a component at the specefied path
        /// </summary>
        /// <param name="path">path as string array e.g. ["src","some","path"]</param>
        /// <param name="component">Componet to insert</param>
        /// <returns>Returns the inserted component</returns>
        public abstract TreeComponent InsertComponentAt(string[] path, TreeComponent component);

        /// <summary>
        /// Max value of the defined metic in whole tree
        /// </summary>
        /// <param name="MetricKey">sonarqube metric key</param>
        /// <returns>max value</returns>
        public abstract float GetMaxForMetric(string MetricKey);

        /// <summary>
        /// Update the Information in this Component. This is necessary because a folder 
        /// is generated for a file to insert and later the folder component 
        /// with the information is inserted. 
        /// </summary>
        /// <param name="component">Component that is used to update</param>
        /// <returns>Updated component</returns>
        public virtual TreeComponent UpdateComponent(TreeComponent component)
        {
            if (component != null && Name == component.Name)
            {
                ID = component.ID;
                Key = component.Key;
                Name = component.Name;
                Path = component.Path;
                Qualifier = component.Qualifier;
                Metrics = component.Metrics;
                return this;
            }
            return null;
        }

        /// <summary>
        /// Helper method to transorm the response of a SQ API request to the internal TreeMetric
        /// </summary>
        /// <param name="measures">List of SQ response measures</param>
        /// <returns>List of tree metrics</returns>
        protected List<TreeMetric> TransformToTreeMetrics(List<Measure> measures)
        {
            List<TreeMetric> m = new List<TreeMetric>();
            foreach (Measure measure in measures)
            {
                try
                {
                    double d = Convert.ToDouble(measure.value);
                    m.Add(new TreeMetric(measure.metric, (float)d));
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            }
            return m;
        }

        /// <summary>
        /// As the name suggests this method returns a representation of this class as string
        /// </summary>
        /// <returns>The representation of this class as string</returns>
        public override string ToString()
        {
            string res = Name + "\n\t" + Path + "\n\t";
            foreach (TreeMetric tm in Metrics)
            {
                res += tm.ToString();
            }
            return res;
        }

        /// <summary>
        /// Helper method to return a sub array of a predefined array
        /// </summary>
        /// <typeparam name="T">Type of array</typeparam>
        /// <param name="data">Array you want a sub array of</param>
        /// <param name="startIndex">Index the SubArray starts</param>
        /// <param name="length">Length of the new array</param>
        /// <returns>SubArray</returns>
        public static T[] SubArray<T>(T[] data, int startIndex, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, startIndex, result, 0, length);
            return result;
        }

        /// <summary>
        /// Helper method to get the SqQualifier enum for the SQ qualifier string
        /// </summary>
        /// <param name="qualifier">SQ qualifier string</param>
        /// <returns>SqQualifier enum</returns>
        public static SqQualifier QualifierForString(string qualifier)
        {
            switch (qualifier)
            {
                case "BRC": return SqQualifier.SUB_PROJECT;
                case "DIR": return SqQualifier.DIRECTORY;
                case "FIL": return SqQualifier.FILE;
                case "TRK": return SqQualifier.PROJECT;
                case "UTS": return SqQualifier.UNIT_TEST;
                default: throw new ArgumentException("Unknown Argument for Qualifier: \"" + qualifier + "\"");
            }
        }
    }
}
