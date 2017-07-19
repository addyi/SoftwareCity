using System;
using System.Collections.Generic;
using Webservice.Response.ComponentTree;
using DataModel.ProjectTree.Components;

namespace DataModel.ProjectTree
{
    interface IProjectTree
    {
        ProjectComponent BuildProjectTree(SqComponent baseComponent, List<SqComponent> components);
        ProjectComponent GetTree();
    }
}
