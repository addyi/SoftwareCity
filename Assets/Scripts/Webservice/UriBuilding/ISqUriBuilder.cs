using System;

namespace Webservice.UriBuilding
{
    public interface ISqUriBuilder
    {
        ISqUriBuilder UserCredentials(string username, string password);
        ISqUriBuilder ProjectKey(string projektKey);
        ISqUriBuilder MetricKeys(string metricKeys);
        ISqUriBuilder Page(int page);
        ISqUriBuilder PageSize(int pageSize);
        ISqUriBuilder AppendToPath(string pathToAppend);
        ISqUriBuilder AppendToQuery(string queryToAppend);

        Uri GetSqUri();
    }
}
