using System;

namespace Webservice.UriBuilding
{
    /// <summary>
    /// Helper class to build the uri for the SonarQube API request
    /// </summary>
    public class SqUriBuilder
    {
        /// <summary>
        /// UriBuilder used for the creaton of the uri
        /// </summary>
        protected UriBuilder uriBuilder;

        /// <summary>
        /// Base constructor for the SQUriBuilder
        /// </summary>
        /// <param name="baseUri">Base Uri e.g. http://sonarqube.eosn.de/ (without /api)</param>
        public SqUriBuilder(string baseUri)
        {
            uriBuilder = new UriBuilder(baseUri);
        }

        /// <summary>
        /// Append Path to Uri e.g. api/some/path/
        /// </summary>
        /// <param name="pathToAppend">Path that will be appended</param>
        /// <returns>Same Obj for method chaining</returns>
        public SqUriBuilder AppendToPath(string pathToAppend)
        {
            if (pathToAppend == null || pathToAppend == "")
                return this;
            if (uriBuilder.Path != null && uriBuilder.Path.Length > 1)
                uriBuilder.Path = uriBuilder.Path.Substring(0) + "/" + pathToAppend;
            else
                uriBuilder.Path = pathToAppend;
            return this;
        }

        /// <summary>
        /// Append Query to Uri e.g. key=value
        /// </summary>
        /// <param name="queryToAppend">Query that will be appended</param>
        /// <returns>Same Obj for method chaining</returns>
        public SqUriBuilder AppendToQuery(string queryToAppend)
        {
            if (queryToAppend == null || queryToAppend == "")
                return this;
            if (uriBuilder.Query != null && uriBuilder.Query.Length > 1)
                uriBuilder.Query = uriBuilder.Query.Substring(1) + "&" + queryToAppend;
            else
                uriBuilder.Query = queryToAppend;
            return this;
        }

        /// <summary>
        /// Setting the credentials for basic auth procedure
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>Same Obj for method chaining</returns>
        public SqUriBuilder UserCredentials(string username, string password)
        {
            if (username == null || password == null || username == "" || password == "")
                return this;

            uriBuilder.Password = password;
            uriBuilder.UserName = username;
            return this;
        }

        /// <summary>
        /// Returns the fully assembled Uri
        /// </summary>
        /// <returns>Fully assembled Uri</returns>
        public Uri GetSqUri()
        {
            return uriBuilder.Uri;
        }
    }
}
