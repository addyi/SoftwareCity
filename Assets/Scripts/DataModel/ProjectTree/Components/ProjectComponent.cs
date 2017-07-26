using System;
using Webservice.Response.ComponentTree;

namespace DataModel.ProjectTree.Components
{
    /// <summary>
    /// Project representation this is the root of the tree
    /// </summary>
    [Serializable]
    public class ProjectComponent : DirComponent
    {
        /// <summary>
        /// Default constructor, needed because of the Serialization to disk
        /// </summary>
        public ProjectComponent() { }

        /// <summary>
        /// This is the constructor normaly used to instanciate a fully fledged obj
        /// </summary>
        /// <param name="component">SqComponent from the SQ API response</param>
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

        /// <summary>
        /// Update the Information in this Component. This is necessary because a folder 
        /// is generated for a file to insert and later the folder component 
        /// with the information is inserted. 
        /// </summary>
        /// <param name="component">Component that is used to update</param>
        /// <returns>Updated component</returns>
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
