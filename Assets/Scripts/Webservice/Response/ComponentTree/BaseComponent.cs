using System;
using System.Collections.Generic;


namespace Webservice.Response.ComponentTree
{
    [Serializable]
    public class BaseComponent
    {
        public string id;
        public string key;
        public string name;
        public string qualifier;
        public List<Measure> measures;

        public override string ToString()
        {
            return string.Format("(BaseComponent: id={0}, key={1}, name={2}, qualifier={3})", id, key, name, qualifier);
        }
    }
}
