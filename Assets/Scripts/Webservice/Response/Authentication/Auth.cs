using System;

namespace Webservice.Response.Authentication
{
    [Serializable]
    class Auth
    {
        public bool valid;

        public override string ToString()
        {
            return string.Format("(Auth: valid={0})",valid);
        }
    }
}
