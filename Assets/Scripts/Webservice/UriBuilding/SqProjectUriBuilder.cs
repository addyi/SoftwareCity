namespace Webservice.UriBuilding
{
    /// <summary>
    /// Helper class to build the uri for the SQ API 
    /// request of a list of viewable projects
    /// </summary>
    public class SqProjectUriBuilder : SqUriBuilder
    {
        /// <summary>
        /// Base constructor for the SqProjectUriBuilder
        /// </summary>
        /// <param name="baseUri">Base Uri e.g. http://sonarqube.eosn.de/ (without /api)</param>
        public SqProjectUriBuilder(string baseUri) : base(baseUri)
        {
            AppendToPath("api/projects/index");
        }
    }
}
