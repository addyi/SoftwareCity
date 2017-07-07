using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Webservice.Response.ComponentTree;
using DataModel.ProjectTree;
using DataModel.ProjectTree.Components;

namespace DataModel.ProjectTree
{
    interface IProjectTree
    {
        ProjectComponent BuildProjectTree(Component baseComponent, List<Component> components);
        // ProjectMetrics GetProjectMetrics();
         ProjectComponent GetTree();
        // TODO ADDYI weitere project methodes
    }
}
