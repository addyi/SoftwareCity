using System;
using Webservice.Response.ComponentTree;

namespace DataModel.ProjectTree.Components
{
    class ProjectComponent : DirComponent
    {
        public ProjectComponent(Component component) : base(component)
        {
            if (component.qualifier == "TRK")
            {
                this.ID = component.id;
                this.Key = component.key;
                this.Name = component.name;
                this.Qualifier = SqQualifier.PROJECT;
                // path ist IMMER "" (empty)
                this.Path = "";
                // TODO ADDYI  this.Metrics = component.measures;
            }
            else
            {
                throw new ArgumentException("Illegal Argument for Qualifier: \"" + component.qualifier + "\"");
            }
        }



    }
}
