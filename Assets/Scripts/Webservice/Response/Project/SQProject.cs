using System;

namespace Webservice.Response.Project
{
    [Serializable]
    public class SQProject
    {

        public string k;

        public string nm;

        public override string ToString()
        {
            return string.Format("(SQProject:  key={0}, name={1})", k, nm);
        }
    }
}
