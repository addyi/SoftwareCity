using System;
using System.Collections.Generic;

namespace Webservice.Response.ComponentTree
{
    /// <summary>
    /// This is a POCO for JOSN deserialization of the ComponentTree response
    /// </summary>
    [Serializable]
    class ComponentTree
    {
        /// <summary>
        /// Infos about the JSON response pages
        /// </summary>
        public Paging paging;
        /// <summary>
        /// Infos about the whole requestet project
        /// </summary>
        public SqComponent baseComponent;
        /// <summary>
        /// List of all the components in the project
        /// </summary>
        public List<SqComponent> components;

        /// <summary>
        /// As the name suggests this method returns a representation of this class as string
        /// </summary>
        /// <returns>The representation of this class as string</returns>
        public override string ToString()
        {
            return string.Format("(ComponentTree: Paging={0}, BaseComponent={1})",
                paging.ToString(), baseComponent.ToString());
        }
    }
}
