using System;
using System.Collections.Generic;
using DataModel.ProjectTree.Components;
using DataModel.ProjectTree;
using Webservice.Response.ComponentTree;
using DataModel.Metrics;
using DataModel.SelectedMetrics;
using DiskIO.AvailableMetrics;

namespace DataModel
{
    public enum SqQualifier { SUB_PROJECT, DIRECTORY, FILE, PROJECT, UNIT_TEST }

    class Model : IProjectTree, IAvailableMetrics, ISelectedMetrics
    {
        private readonly static Model model = new Model();
        private ProjectComponent project;
        private Metric[] SelectedMetrics;

        private Model() { }

        public static Model GetInstance() { return model; }

        public ProjectComponent BuildProjectTree(SqComponent baseComponent,
            List<SqComponent> components)
        {
            if (baseComponent == null || components == null)
                return null;

            project = new ProjectComponent(baseComponent);

            foreach (SqComponent c in components)
            {
                string[] s = c.path.Split('/');
                project.InsertComponentAt(s, GetTreeComponent(c));
            }
            return project;
        }

        public Metric[] GetSelectedMetrics()
        {
            return SelectedMetrics;
        }

        public ProjectComponent GetTree() { return project; }

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
