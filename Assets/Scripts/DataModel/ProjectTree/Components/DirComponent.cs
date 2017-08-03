using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Webservice.Response.ComponentTree;

namespace DataModel.ProjectTree.Components
{
    /// <summary>
    /// Directory representation in the tree
    /// </summary>
    [Serializable]
    public class DirComponent : TreeComponent
    {
        /// <summary>
        /// List with all the sub components in this component e.g. File, Dir
        /// </summary>
        public List<TreeComponent> components = new List<TreeComponent>();

        /// <summary>
        /// Default constructor, needed because of the Serialization to disk
        /// </summary>
        public DirComponent() { }

        /// <summary>
        /// This is the constructor normaly used to instanciate a fully fledged obj
        /// </summary>
        /// <param name="component">SqComponent from the SQ API response</param>
        public DirComponent(SqComponent component) : base(component)
        {
            if (Qualifier != SqQualifier.DIRECTORY
                && Qualifier != SqQualifier.PROJECT
                && Qualifier != SqQualifier.SUB_PROJECT)
                throw new ArgumentException("Illegal Argument for Qualifier: \"" + component.qualifier + "\"");
        }

        /// <summary>
        /// This is the constructor if a folder is needed, but no SqComponent is available
        /// </summary>
        /// <param name="dirName">name for the dir component</param>
        private DirComponent(string dirName) : base(dirName) { }

        /// <summary>
        /// Insert/Update a component at the specefied path
        /// </summary>
        /// <param name="path">path as string array e.g. ["src","some","path"]</param>
        /// <param name="component">Componet to insert</param>
        /// <returns>Returns the inserted component</returns>
        public override TreeComponent InsertComponentAt(string[] path, TreeComponent component)
        {
            if (component == null || path == null || path.Length < 0)
                return null;

            if (path.Length == 0)
                return UpdateComponent(component);

            TreeComponent tc = FindSubComponentInNode(path[0]);

            if (tc != null)
                return tc.InsertComponentAt(SubArray(path, 1, path.Length - 1), component);

            if (path.Length == 1)
            {
                components.Add(component);
                return component;
            }

            tc = new DirComponent(path[0]);
            tc.Qualifier = SqQualifier.UNDEFINED;
            components.Add(tc);
            return tc.InsertComponentAt(SubArray(path, 1, path.Length - 1), component);
        }

        /// <summary>
        /// Max value of the defined metic in whole tree
        /// </summary>
        /// <param name="MetricKey">sonarqube metric key</param>
        /// <returns>max value</returns>
        public override float GetMaxForMetric(string MetricKey)
        {
            float res = 0;
            foreach (TreeComponent tc in components)
            {
                float componentMax = tc.GetMaxForMetric(MetricKey);
                if (componentMax > res)
                {
                    res = componentMax;
                }
            }
            return res;
        }

        public TreeComponent FindSubComponentInNode(string componentName)
        {
            foreach (TreeComponent c in components)
            {
                if (c.Name == componentName)
                    return c;
            }
            return null;
        }

        /// <summary>
        /// Update the Information in this Component. This is necessary because a folder 
        /// is generated for a file to insert and later the folder component 
        /// with the information is inserted. 
        /// </summary>
        /// <param name="component">Component that is used to update</param>
        /// <returns>Updated component</returns>
        public override TreeComponent UpdateComponent(TreeComponent component)
        {
            if (component != null && component is DirComponent && Name == component.Name)
            {
                base.UpdateComponent(component);
                return this;
            }
            return null;
        }

        /// <summary>
        /// As the name suggests this method returns a representation of this class as string
        /// </summary>
        /// <returns>The representation of this class as string</returns>
        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append(base.ToString());
            foreach (TreeComponent tc in components)
            {
                s.Append("\n" + tc.ToString());
            }
            return s.ToString();
        }
    }
}
