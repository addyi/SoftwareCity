using System;
using Webservice.Response.Project;

namespace Webservice.Response.ArrayResponseSQProject
{
    /// <summary>
    /// This is a POCO for JOSN deserialization of an array with SQProjects
    /// </summary>
    [Serializable]
    class ArrayResponseSQProject
    {
        /// <summary>
        /// Array of all the requestable SQProjects
        /// </summary>
        public SQProject[] array = default(SQProject[]);
    }
}
