using System.Collections.Generic;
using Webservice.Response.ComponentTree;
using DataModel.ProjectTree.Components;

namespace DataModel.ProjectTree
{
    /// <summary>
    /// Methods the Model needs to provide for ProjectTree handling
    /// </summary>
    interface IProjectTree
    {
        /// <summary>
        /// Constructs the component tree of the baseComponent and a list of components.
        /// </summary>
        /// <param name="baseComponent">Project Infos & Metrics</param>
        /// <param name="components">List of Components</param>
        /// <returns>Root of ComponentTree</returns>
        ProjectComponent BuildProjectTree(SqComponent baseComponent, List<SqComponent> components);
        /// <summary>
        /// Returns the root of the project tree
        /// </summary>
        /// <returns>root of the project tree</returns>
        ProjectComponent GetTree();
        /// <summary>
        /// Sets the root of the project tree e.g. if a tree is read from disk
        /// </summary>
        /// <param name="tree">Root of the project tree</param>
        void SetTree(ProjectComponent tree);
        /// <summary>
        /// Sets the tree to null
        /// </summary>
        void DeleteTree();
    }
}
