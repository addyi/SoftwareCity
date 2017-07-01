

using System;

namespace Webservice.UriBuilding
{
    class SqProjectUriBuilder : SqUriBuilder
    {
        public SqProjectUriBuilder(string baseUri) : base(baseUri)
        {
            AppendToPath("api/projects/index");
        }

        public override ISqUriBuilder MetricKeys(string metricKeys)
        {
            return this;
        }

        public override ISqUriBuilder Page(int page)
        {
            return this;
        }

        public override ISqUriBuilder PageSize(int pageSize)
        {
            return this;
        }

        public override ISqUriBuilder ProjectKey(string projektKey)
        {
            return this;
        }
    }
}
