using System;

namespace Webservice.UriBuilding
{
    class SqComponentTreeUriBuilder : SqUriBuilder
    {
        public SqComponentTreeUriBuilder(string baseUri, string projectKey, string metricKeys) : base(baseUri)
        {
            AppendToPath("api/measures/component_tree");
            ProjectKey(projectKey);
            MetricKeys(metricKeys);
        }

        public override ISqUriBuilder MetricKeys(string metricKeys)
        {
            return AppendToQuery(string.Format("metricKeys={0}", metricKeys));
        }

        public override ISqUriBuilder Page(int page)
        {
            return AppendToQuery(string.Format("p={0}", page));
        }

        public override ISqUriBuilder PageSize(int pageSize)
        {
            if (pageSize <= 500 && pageSize > 0)
            {
                return AppendToQuery(string.Format("ps={0}", pageSize));
            }
            return this;
        }

        public override ISqUriBuilder ProjectKey(string projectKey)
        {
            return AppendToQuery(string.Format("baseComponentKey={0}", projectKey));
        }
    }
}
