namespace Webservice.UriBuilding
{
    /// <summary>
    /// Helper class to build the uri for the SQ API 
    /// request if the credentials are valid
    /// </summary>
    class SqAuthValidationUriBuilder : SqUriBuilder
    {
        /// <summary>
        /// Base constructor for the SqAuthValidationUriBuilder
        /// </summary>
        /// <param name="baseUri">Base Uri e.g. http://sonarqube.eosn.de/ (without /api)</param>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        public SqAuthValidationUriBuilder(string baseUri, string username, string password) : base(baseUri)
        {
            AppendToPath("api/authentication/validate");
            UserCredentials(username, password);
        }
    }
}
