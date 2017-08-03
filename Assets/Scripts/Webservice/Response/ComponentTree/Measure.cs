using System;

namespace Webservice.Response.ComponentTree
{
    /// <summary>
    /// This is a POCO for JOSN deserialization of SonarQube Metrics
    /// </summary>
    [Serializable]
    public class Measure
    {
        /// <summary>
        /// Key of the Metric
        /// </summary>
        public string metric;
        /// <summary>
        /// Value of the Metric
        /// </summary>
        public string value;

        /// <summary>
        /// As the name suggests this method returns a representation of this class as string
        /// </summary>
        /// <returns>The representation of this class as string</returns>
        public override string ToString()
        {
            return string.Format("(Measure: Metric={0}, Value={1})", metric, value);
        }
    }
}
