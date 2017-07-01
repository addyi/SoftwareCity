using System;

namespace Webservice.Response.ComponentTree
{
    [Serializable]
    public class Measure
    {
        public string metric;
        public string value;

        public override string ToString()
        {
            return string.Format("(Measure: Metric={0}, Value={1})", metric, value);
        }
    }
}
