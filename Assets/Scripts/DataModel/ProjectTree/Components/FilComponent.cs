using System;
using Webservice.Response.ComponentTree;

namespace DataModel.ProjectTree.Components
{
    /// <summary>
    /// File representation e.g. .java, .xml. But no unit tests.
    /// </summary>
    [Serializable]
    public class FilComponent : TreeLeafComponent
    {
        /// <summary>
        /// Default constructor, needed because of the Serialization to disk
        /// </summary>
        public FilComponent() { }

        /// <summary>
        /// This is the constructor normaly used to instanciate a fully fledged obj
        /// </summary>
        /// <param name="component">SqComponent from the SQ API response</param>
        public FilComponent(SqComponent component) : base(component)
        {
            if (Qualifier != SqQualifier.FILE)
                throw new ArgumentException("Illegal Argument for Qualifier: \"" + component.qualifier + "\"");
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
            if (component is FilComponent && Name == component.Name)
            {
                FilComponent f = (FilComponent)component;
                base.UpdateComponent(component);
                Language = f.Language;
                return this;
            }
            return null;
        }
    }
}
