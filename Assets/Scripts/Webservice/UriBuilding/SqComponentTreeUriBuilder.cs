using System;

namespace Webservice.UriBuilding
{
    /// <summary>
    /// Helper class to build the uri for the SQ API 
    /// request of the component tree
    /// </summary>
    public class SqComponentTreeUriBuilder : SqUriBuilder
    {
        /// <summary>
        /// page number for the sq api request 
        /// </summary>
        private int page = 0;

        /// <summary>
        /// size of the page (number of components per request) [Min 1; Max 500]
        /// </summary>
        private int pageSize = 0;

        /// <summary>
        /// Base constructor for the SqComponentTreeUriBuilder
        /// </summary>
        /// <param name="baseUri">Base Uri e.g. 
        /// http://sonarqube.eosn.de/ (without /api)</param>
        /// <param name="projectKey">SonarQube key for the requested project</param>
        /// <param name="metricKeys">List of key for the requested metrics 
        /// e.g. "ncloc,bugs,vulnerabilities,..."</param>
        /// <exception cref="ArgumentException">metricKeys and projectKey mustn't be empty string</exception>
        /// <exception cref="ArgumentNullException">metricKeys and projectKey mustn't be null</exception>
        public SqComponentTreeUriBuilder(string baseUri, string projectKey,
            string metricKeys) : base(baseUri)
        {
            if (metricKeys == null)
                throw new ArgumentNullException(metricKeys);
            if (metricKeys == "")
                throw new ArgumentException("metricKeys mustn't be empty string");
            if (projectKey == null)
                throw new ArgumentNullException(projectKey);
            if (projectKey == "")
                throw new ArgumentException("projectKey mustn't be empty string");

            AppendToPath("api/measures/component_tree");
            AppendToQuery(string.Format("baseComponentKey={0}", projectKey));
            AppendToQuery(string.Format("metricKeys={0}", metricKeys));
        }

        /// <summary>
        /// Set the page number for the request
        /// </summary>
        /// <param name="page">page number for the request (min 1)</param>
        /// <returns>Same Obj for method chaining</returns>
        public SqComponentTreeUriBuilder Page(int page)
        {
            if (page > 0)
            {
                this.page = page;
            }
            return this;
        }

        /// <summary>
        /// Set the size of the page (number of components per request)
        /// </summary>
        /// <param name="pageSize">size of the page (min 1; max 500)</param>
        /// <returns>Same Obj for method chaining</returns>
        public SqComponentTreeUriBuilder PageSize(int pageSize)
        {
            if (pageSize <= 500 && pageSize > 0)
            {
                this.pageSize = pageSize;
            }
            return this;
        }

        /// <summary>
        /// Returns the fully assembled Uri
        /// </summary>
        /// <returns>Fully assembled Uri</returns>
        public Uri GetSqUri()
        {
            if (pageSize <= 500 && pageSize > 0)
                AppendToQuery(string.Format("ps={0}", pageSize));
            if (page > 0)
                AppendToQuery(string.Format("p={0}", page));

            return base.GetSqUri();
        }
    }
}
