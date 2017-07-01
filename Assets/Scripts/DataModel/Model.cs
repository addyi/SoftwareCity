using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel;
using Webservice.Response.ComponentTree;
using DataModel.ProjectTree;

namespace DataModel
{
    class Model : IProjectTree
    {
        private ProjectInfos project;

        public void BuildProjectTree(BaseComponent baseComponent, List<Component> components)
        {
            this.project = new ProjectInfos(baseComponent);
        }

        public ProjectInfos GetProjectInfos()
        {
            return project;
        }
    }
}
