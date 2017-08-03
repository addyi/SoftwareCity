using System.Collections.Generic;
using System;

namespace Webservice.Response.ComponentTree
{
    /// <summary>
    /// This is a POCO for JOSN deserialization of SonarQube components
    /// </summary>
    [Serializable]
    public class SqComponent
    {
        /// <summary>
        /// Id of the component
        /// </summary>
        public string id;
        /// <summary>
        /// Key of the component
        /// </summary>
        public string key;
        /// <summary>
        /// Name of the component
        /// </summary>
        public string name;
        /// <summary>
        /// Qualifier of the component (BRC, DIR, FIL, TRK, UTS)
        /// look at SQ API Doku
        /// </summary>      
        public string qualifier;
        /// <summary>
        /// Path of the component
        /// </summary>
        public string path;
        /// <summary>
        /// language of the component e.g. java, xml
        /// </summary>
        public string language;
        /// <summary>
        /// List of the Measures of the component
        /// </summary>
        public List<Measure> measures;

        /// <summary>
        /// As the name suggests this method returns a representation of this class as string
        /// </summary>
        /// <returns>The representation of this class as string</returns>
        public override string ToString()
        {
            return string.Format("(Component: id={0}, key={1}, name={2}, qualifier={3}, " +
                "path={4}, language={5}, measures={6})", id, key, name,
                qualifier, path, language, measures);
        }
    }
}
