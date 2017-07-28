namespace Webservice.UriBuilding
{
    /// <summary>
    /// Helper class to build the uri for the SQ API 
    /// request of the component tree
    /// </summary>
    public class SqComponentTreeUriBuilder : SqUriBuilder
    {
        /// <summary>
        /// Base constructor for the SqComponentTreeUriBuilder
        /// </summary>
        /// <param name="baseUri">Base Uri e.g. 
        /// http://sonarqube.eosn.de/ (without /api)</param>
        /// <param name="projectKey">SonarQube key for the requested project</param>
        /// <param name="metricKeys">List of key for the requested metrics 
        /// e.g. "ncloc,bugs,vulnerabilities,..."</param>
        public SqComponentTreeUriBuilder(string baseUri, string projectKey,
            string metricKeys) : base(baseUri)
        {
            AppendToPath("api/measures/component_tree");
            ProjectKey(projectKey);
            MetricKeys(metricKeys);
        }

        /// <summary>
        /// Define the metrics for the request
        /// </summary>
        /// <param name="metricKeys">List of key for the requested metrics 
        /// e.g. "ncloc,bugs,vulnerabilities,..."</param>
        private void MetricKeys(string metricKeys)
        {
            AppendToQuery(string.Format("metricKeys={0}", metricKeys));
        }

        /// <summary>
        /// Set the page number for the request
        /// </summary>
        /// <param name="page">page number for the request</param>
        /// <returns>Same Obj for method chaining</returns>
        public SqUriBuilder Page(int page)
        {
            return AppendToQuery(string.Format("p={0}", page));
        }

        /// <summary>
        /// Set the size of the page (number of components per request)
        /// </summary>
        /// <param name="pageSize">size of the page</param>
        /// <returns>Same Obj for method chaining</returns>
        public SqUriBuilder PageSize(int pageSize)
        {
            if (pageSize <= 500 && pageSize > 0)
            {
                return AppendToQuery(string.Format("ps={0}", pageSize));
            }
            return this;
        }

        /// <summary>
        /// Define the projectKey for the request
        /// </summary>
        /// <param name="projectKey">SonarQube key for the requested project</param>
        private void ProjectKey(string projectKey)
        {
            AppendToQuery(string.Format("baseComponentKey={0}", projectKey));
        }
    }
}
