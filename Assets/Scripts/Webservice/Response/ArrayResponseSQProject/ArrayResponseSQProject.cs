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
        public SQProject[] array = default(SQProject[]);
    }
}
