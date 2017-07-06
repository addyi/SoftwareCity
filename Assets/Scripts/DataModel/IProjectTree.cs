using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Webservice.Response.ComponentTree;
using DataModel.ProjectTree;
using DataModel.ProjectTree.Components;

namespace DataModel
{
    interface IProjectTree
    {
        void BuildProjectTree(Component baseComponent, List<Component> components);
        // ProjectMetrics GetProjectMetrics();
         ProjectComponent GetTree();
        // TODO ADDYI weitere project methodes
    }
}
