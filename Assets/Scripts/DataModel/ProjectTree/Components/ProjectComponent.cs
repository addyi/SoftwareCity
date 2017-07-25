using System;
using System.Linq;
using Webservice.Response.ComponentTree;

namespace DataModel.ProjectTree.Components
{
    [Serializable]
    public class ProjectComponent : DirComponent
    {
        public ProjectComponent() { }

        public ProjectComponent(SqComponent component) : base(component)
        {
            if (component.qualifier == "TRK")
            {
                Qualifier = SqQualifier.PROJECT;
                // path ist IMMER "" (empty)
                Path = "";
            }
            else
            {
                throw new ArgumentException("Illegal Argument for Qualifier: \"" + component.qualifier + "\"");
            }
        }

        public override TreeComponent UpdateComponent(TreeComponent component)
        {
            if (component != null && component is ProjectComponent && Name == component.Name)
            {
                base.UpdateComponent(component);
                return this;
            }
            return null;
        }

    }
}
