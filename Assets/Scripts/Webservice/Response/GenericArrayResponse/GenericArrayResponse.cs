using System;
using System.Collections.Generic;
using Webservice.Response.Project;

namespace Webservice.Response.ArrayResponseSQProject
{
    [Serializable]
    class ArrayResponseSQProject
    {
        public SQProject[] array = default(SQProject[]);
    }
}
