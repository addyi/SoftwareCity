using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Webservice.Response.ComponentTree;

namespace DataModel.ProjectTree.Components
{
    [Serializable]
    public class DirComponent : TreeComponent
    {
        public List<TreeComponent> components = new List<TreeComponent>();

        public DirComponent(SqComponent component) : base(component)
        {
            if (Qualifier != SqQualifier.DIRECTORY
                && Qualifier != SqQualifier.PROJECT
                && Qualifier != SqQualifier.SUB_PROJECT)
                throw new ArgumentException("Illegal Argument for Qualifier: \"" + component.qualifier + "\"");
        }

        private DirComponent(string dirName) : base(dirName) { }

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
                //TODO ADDYI FIX SORT ELSE EXCEPTION components.Sort();
                return component;
            }

            tc = new DirComponent(path[0]);
            tc.Qualifier = SqQualifier.UNDEFINED;
            components.Add(tc);
            //TODO ADDYI FIX SORT ELSE EXCEPTION components.Sort();
            return tc.InsertComponentAt(SubArray(path, 1, path.Length - 1), component);
        }

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

        public override TreeComponent UpdateComponent(TreeComponent component)
        {
            if (component != null && component is DirComponent && Name == component.Name)
            {
                base.UpdateComponent(component);
                return this;
            }
            return null;
        }

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
