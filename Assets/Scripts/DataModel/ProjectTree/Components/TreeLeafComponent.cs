using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Webservice.Response.ComponentTree;

namespace DataModel.ProjectTree.Components
{
    abstract class TreeLeafComponent : TreeComponent
    {
        public string Language;

        protected TreeLeafComponent(SqComponent component):base(component)
        {
            Language = component.language;            
        }

        public override TreeComponent InsertComponentAt(string[] path, TreeComponent component)
        {
            if (component == null || path == null || path.Length <= 0)
                return null;
            if (path.Length == 0 && Name == component.Name)
                return UpdateComponent(component);
            return null;
        }

        
    }
}
