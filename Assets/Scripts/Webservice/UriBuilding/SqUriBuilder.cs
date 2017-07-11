using System;

namespace Webservice.UriBuilding
{
    public abstract class SqUriBuilder : ISqUriBuilder
    {
        protected UriBuilder uriBuilder;

        protected SqUriBuilder(string baseUri)
        {
            uriBuilder = new UriBuilder(baseUri);
        }

        public ISqUriBuilder AppendToPath(string pathToAppend)
        {
            if (uriBuilder.Path != null && uriBuilder.Path.Length > 1)
                uriBuilder.Path = uriBuilder.Path.Substring(1) + "/" + pathToAppend;
            else
                uriBuilder.Path = pathToAppend;
            return this;
        }

        public ISqUriBuilder AppendToQuery(string queryToAppend)
        {
            if (uriBuilder.Query != null && uriBuilder.Query.Length > 1)
                uriBuilder.Query = uriBuilder.Query.Substring(1) + "&" + queryToAppend;
            else
                uriBuilder.Query = queryToAppend;
            return this;
        }

        public ISqUriBuilder UserCredentials(string username, string password)
        {
            uriBuilder.Password = password;
            uriBuilder.UserName = username;
            return this;
        }
        public Uri GetSqUri()
        {
            return uriBuilder.Uri;
        }

        public abstract ISqUriBuilder MetricKeys(string metricKeys);
        public abstract ISqUriBuilder Page(int page);
        public abstract ISqUriBuilder PageSize(int pageSize);
        public abstract ISqUriBuilder ProjectKey(string projektKey);

    }
}
