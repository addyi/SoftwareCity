using System;

namespace Webservice.Response.Project
{
    /// <summary>
    /// This is a POCO for JOSN deserialization of SQProjects
    /// </summary>
    [Serializable]
    public class SQProject : IComparable
    {
        /// <summary>
        /// Key of the project
        /// </summary>
        public string k;

        /// <summary>
        /// Name of the project
        /// </summary>
        public string nm;

        public int CompareTo(object obj)
        {
            if (this == obj)
            {
                return 0;
            }
            if (!(this is SQProject))
            {
                return -1;
            }
            return this.nm.CompareTo(((SQProject)obj).nm);
        }

        /// <summary>
        /// As the name suggests this method returns a representation of this class as string
        /// </summary>
        /// <returns>The representation of this class as string</returns>
        public override string ToString()
        {
            return string.Format("(SQProject:  key={0}, name={1})", k, nm);
        }
    }
}
