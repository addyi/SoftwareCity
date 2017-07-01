using System.Collections.Generic;
using System;

namespace Webservice.Response.ComponentTree
{
    [Serializable]
    public class Component
    {
        public string id;
        public string key;
        public string name;
        public string qualifier;
        public string path;
        public string language;
        public List<Measure> measures;

        public override string ToString()
        {
            return string.Format("(Component: id={0}, key={1}, name={2}, qualifier={3}, path={4}, language={5}, measures={6})", id, key, name, qualifier, path, language, measures);
        }

    }
}
