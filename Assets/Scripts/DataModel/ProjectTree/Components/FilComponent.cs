using System;
using Webservice.Response.ComponentTree;

namespace DataModel.ProjectTree.Components
{
    [Serializable]
    public class FilComponent : TreeLeafComponent
    {
        public FilComponent(SqComponent component) : base(component)
        {
            if (Qualifier != SqQualifier.FILE)
                throw new ArgumentException("Illegal Argument for Qualifier: \"" + component.qualifier + "\"");
        }

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
