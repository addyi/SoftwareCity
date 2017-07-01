using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Webservice.Response.ComponentTree;
using DataModel.ProjectTree;

namespace DataModel
{
    interface IProjectTree
    {
        void BuildProjectTree(BaseComponent baseComponent, List<Component> components);
        // ProjectMetrics GetProjectMetrics();
        ProjectInfos GetProjectInfos();
        // TODO ADDYI weitere project methodes
    }
}
