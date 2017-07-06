using System;
using System.Collections.Generic;
using DataModel.ProjectTree.Components;
using Webservice.Response.ComponentTree;

namespace DataModel
{
    public enum SqQualifier { SUB_PROJECT, DIRECTORY, FILE, PROJECT, UNIT_TEST }

    class Model : IProjectTree
    {
        private ProjectComponent project;


        public void BuildProjectTree(Component baseComponent,
            List<Component> components)
        {
            if (baseComponent == null || components == null)
                return;

            project = new ProjectComponent(baseComponent);
           
            foreach (Component c in components)
            {
                string[] s = c.path.Split('/');
               project.InsertComponentAt(s, GetTreeComponent(c));
            }

        }

        public ProjectComponent GetTree()
        {
            return project;
        }

        private TreeComponent GetTreeComponent(Component component)
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
