using System;

namespace Webservice.Response.Authentication
{
    /// <summary>
    /// This is a POCO for JOSN deserialization of the auth response
    /// </summary>
    [Serializable]
    class Auth
    {
        /// <summary>
        /// Validity of the credentials
        /// </summary>
        public bool valid;

        /// <summary>
        /// As the name suggests this method returns a representation of this class as string
        /// </summary>
        /// <returns>The representation of this class as string</returns>
        public override string ToString()
        {
            return string.Format("(Auth: valid={0})", valid);
        }
    }
}
