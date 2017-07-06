using System;
using System.Collections.Generic;



namespace Webservice.Response.ComponentTree
{
    [Serializable]
    class ComponentTree
    {
        public Paging paging;
        public Component baseComponent;
        public List<Component> components;

        public override string ToString()
        {
            return string.Format("(ComponentTree: Paging={0}, BaseComponent={1})",
                paging.ToString(), baseComponent.ToString());
        }

        public void AddComponents(List<Component> additionalComponents)
        {
            lock (components)
            {
                components.AddRange(additionalComponents);
            }
        }
    }
}
