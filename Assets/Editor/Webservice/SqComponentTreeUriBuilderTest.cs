using NUnit.Framework;
using Webservice.UriBuilding;
using System;

class SqComponentTreeUriBuilderTest
{
    [Test]
    public void UriIsSetCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string projectKey = "project-key";
        string metrics = "ncloc,bugs,vulnerabilities";

        SqComponentTreeUriBuilder uri = new SqComponentTreeUriBuilder(baseUri, projectKey, metrics);

        string expectedUri = baseUri + "api/measures/component_tree?baseComponentKey="
            + projectKey + "&metricKeys=" + metrics;
        Assert.AreEqual(expectedUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void ProjectKeyNull()
    {
        string baseUri = "http://sonarqube.test.de/";
        string projectKey = null;
        string metrics = "ncloc,bugs,vulnerabilities";

        Assert.Throws<ArgumentNullException>(() => new SqComponentTreeUriBuilder(baseUri, projectKey, metrics));
    }

    [Test]
    public void ProjectKeyEmpty()
    {
        string baseUri = "http://sonarqube.test.de/";
        string projectKey = "";
        string metrics = "ncloc,bugs,vulnerabilities";

        Assert.Throws<ArgumentException>(() => new SqComponentTreeUriBuilder(baseUri, projectKey, metrics));
    }

    [Test]
    public void MetricKeysNull()
    {
        string baseUri = "http://sonarqube.test.de/";
        string projectKey = "project-key";
        string metrics = null;

        Assert.Throws<ArgumentNullException>(() => new SqComponentTreeUriBuilder(baseUri, projectKey, metrics));
    }

    [Test]
    public void MetricKeysEmpty()
    {
        string baseUri = "http://sonarqube.test.de/";
        string projectKey = "project-key";
        string metrics = "";

        Assert.Throws<ArgumentException>(() => new SqComponentTreeUriBuilder(baseUri, projectKey, metrics));
    }

    [Test]
    public void BaseUriNotEmpty()
    {
        Assert.Throws<UriFormatException>(() => new SqComponentTreeUriBuilder("", "projectKey", "metricKeys"));
    }

    [Test]
    public void BaseUriNotNull()
    {
        Assert.Throws<ArgumentNullException>(() => new SqComponentTreeUriBuilder(null, "projectKey", "metricKeys"));
    }

    [Test]
    public void NullPathIsAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string projectKey = "project-key";
        string metrics = "ncloc,bugs,vulnerabilities";
        string toAppend = "some/path/";
        string alsoToAppend = null;
        SqComponentTreeUriBuilder uri = new SqComponentTreeUriBuilder(baseUri, projectKey, metrics);

        uri.AppendToPath(toAppend);
        uri.AppendToPath(alsoToAppend);

        string expectedUri = baseUri + "api/measures/component_tree" + "/" + toAppend
           + "?baseComponentKey=" + projectKey + "&metricKeys=" + metrics;
        Assert.AreEqual(expectedUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void EmptyPathIsAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string projectKey = "project-key";
        string metrics = "ncloc,bugs,vulnerabilities";
        string toAppend = "some/path/";
        string alsoToAppend = "";
        SqComponentTreeUriBuilder uri = new SqComponentTreeUriBuilder(baseUri, projectKey, metrics);

        uri.AppendToPath(toAppend);
        uri.AppendToPath(alsoToAppend);

        string expectedUri = baseUri + "api/measures/component_tree" + "/" + toAppend
            + "?baseComponentKey=" + projectKey + "&metricKeys=" + metrics;
        Assert.AreEqual(expectedUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void IsPathAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string projectKey = "project-key";
        string metrics = "ncloc,bugs,vulnerabilities";
        string toAppend = "some/path/";
        SqComponentTreeUriBuilder uri = new SqComponentTreeUriBuilder(baseUri, projectKey, metrics);

        uri.AppendToPath(toAppend);

        string expectedUri = baseUri + "api/measures/component_tree" + "/" + toAppend
            + "?baseComponentKey=" + projectKey + "&metricKeys=" + metrics;
        Assert.AreEqual(expectedUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void AreMultiplePathsAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string projectKey = "project-key";
        string metrics = "ncloc,bugs,vulnerabilities";
        string toAppend = "some/path/";
        string alsoToAppend = "other/path/";
        SqComponentTreeUriBuilder uri = new SqComponentTreeUriBuilder(baseUri, projectKey, metrics);

        uri.AppendToPath(toAppend);
        uri.AppendToPath(alsoToAppend);

        string expectedUri = baseUri + "api/measures/component_tree" + "/" + toAppend + alsoToAppend
            + "?baseComponentKey=" + projectKey + "&metricKeys=" + metrics;
        Assert.AreEqual(expectedUri, uri.GetSqUri().ToString());
    }
    
    [Test]
    public void IsQueryAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string projectKey = "project-key";
        string metrics = "ncloc,bugs,vulnerabilities";
        string query = "key=value";
        SqComponentTreeUriBuilder uri = new SqComponentTreeUriBuilder(baseUri, projectKey, metrics);

        uri.AppendToQuery(query);

        string expectedUri = baseUri + "api/measures/component_tree"
           + "?baseComponentKey=" + projectKey + "&metricKeys="
           + metrics + "&" + query;
        Assert.AreEqual(expectedUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void AreMultipleQuerysAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string projectKey = "project-key";
        string metrics = "ncloc,bugs,vulnerabilities";
        string query = "key=value";
        string alsoToAppend = "anotherKey=anotherValue";
        SqComponentTreeUriBuilder uri = new SqComponentTreeUriBuilder(baseUri, projectKey, metrics);

        uri.AppendToQuery(query);
        uri.AppendToQuery(alsoToAppend);

        string expectedUri = baseUri + "api/measures/component_tree"
           + "?baseComponentKey=" + projectKey + "&metricKeys="
           + metrics + "&" + query + "&" + alsoToAppend;
        Assert.AreEqual(expectedUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void NullQueryIsAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string projectKey = "project-key";
        string metrics = "ncloc,bugs,vulnerabilities";
        string query = "key=value";
        string alsoToAppend = null;
        SqComponentTreeUriBuilder uri = new SqComponentTreeUriBuilder(baseUri, projectKey, metrics);

        uri.AppendToQuery(query);
        uri.AppendToQuery(alsoToAppend);

        string expectedUri = baseUri + "api/measures/component_tree"
           + "?baseComponentKey=" + projectKey + "&metricKeys="
           + metrics + "&" + query;
        Assert.AreEqual(expectedUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void EmptyQueryIsAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string projectKey = "project-key";
        string metrics = "ncloc,bugs,vulnerabilities";
        string query = "key=value";
        string alsoToAppend = "";
        SqComponentTreeUriBuilder uri = new SqComponentTreeUriBuilder(baseUri, projectKey, metrics);

        uri.AppendToQuery(query);
        uri.AppendToQuery(alsoToAppend);

        string expectedUri = baseUri + "api/measures/component_tree"
           + "?baseComponentKey=" + projectKey + "&metricKeys="
           + metrics + "&" + query;
        Assert.AreEqual(expectedUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void IsPageAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string projectKey = "project-key";
        string metrics = "ncloc,bugs,vulnerabilities";
        int page = 1;
        SqComponentTreeUriBuilder uri = new SqComponentTreeUriBuilder(baseUri, projectKey, metrics);

        uri.Page(page);

        string expectedUri = baseUri + "api/measures/component_tree"
           + "?baseComponentKey=" + projectKey + "&metricKeys="
           + metrics + "&" + "p=" + page;
        Assert.AreEqual(expectedUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void PageIsNotZero()
    {
        string baseUri = "http://sonarqube.test.de/";
        string projectKey = "project-key";
        string metrics = "ncloc,bugs,vulnerabilities";
        int page = 0;
        SqComponentTreeUriBuilder uri = new SqComponentTreeUriBuilder(baseUri, projectKey, metrics);

        uri.Page(page);

        string expectedUri = baseUri + "api/measures/component_tree"
           + "?baseComponentKey=" + projectKey + "&metricKeys="
           + metrics;
        Assert.AreEqual(expectedUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void PageIsOverridden()
    {
        string baseUri = "http://sonarqube.test.de/";
        string projectKey = "project-key";
        string metrics = "ncloc,bugs,vulnerabilities";
        int page = 2;
        SqComponentTreeUriBuilder uri = new SqComponentTreeUriBuilder(baseUri, projectKey, metrics);

        uri.Page(1);
        uri.Page(page);

        string expectedUri = baseUri + "api/measures/component_tree"
           + "?baseComponentKey=" + projectKey + "&metricKeys="
           + metrics + "&" + "p=" + page;
        Assert.AreEqual(expectedUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void IsPageSizeAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string projectKey = "project-key";
        string metrics = "ncloc,bugs,vulnerabilities";
        int pageSize = 1;
        SqComponentTreeUriBuilder uri = new SqComponentTreeUriBuilder(baseUri, projectKey, metrics);

        uri.PageSize(pageSize);

        string expectedUri = baseUri + "api/measures/component_tree"
           + "?baseComponentKey=" + projectKey + "&metricKeys="
           + metrics + "&" + "ps=" + pageSize;
        Assert.AreEqual(expectedUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void PageSizeIsNotZero()
    {
        string baseUri = "http://sonarqube.test.de/";
        string projectKey = "project-key";
        string metrics = "ncloc,bugs,vulnerabilities";
        int pageSize = 0;
        SqComponentTreeUriBuilder uri = new SqComponentTreeUriBuilder(baseUri, projectKey, metrics);

        uri.PageSize(pageSize);

        string expectedUri = baseUri + "api/measures/component_tree"
           + "?baseComponentKey=" + projectKey + "&metricKeys="
           + metrics;
        Assert.AreEqual(expectedUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void PageSizeIs500()
    {
        string baseUri = "http://sonarqube.test.de/";
        string projectKey = "project-key";
        string metrics = "ncloc,bugs,vulnerabilities";
        int pageSize = 500;
        SqComponentTreeUriBuilder uri = new SqComponentTreeUriBuilder(baseUri, projectKey, metrics);

        uri.PageSize(pageSize);

        string expectedUri = baseUri + "api/measures/component_tree"
           + "?baseComponentKey=" + projectKey + "&metricKeys="
           + metrics + "&" + "ps=" + pageSize;
        Assert.AreEqual(expectedUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void PageSizeIsNot501()
    {
        string baseUri = "http://sonarqube.test.de/";
        string projectKey = "project-key";
        string metrics = "ncloc,bugs,vulnerabilities";
        int pageSize = 501;
        SqComponentTreeUriBuilder uri = new SqComponentTreeUriBuilder(baseUri, projectKey, metrics);

        uri.PageSize(pageSize);

        string expectedUri = baseUri + "api/measures/component_tree"
           + "?baseComponentKey=" + projectKey + "&metricKeys="
           + metrics;
        Assert.AreEqual(expectedUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void PageSizeIsNotOverriddenIfNotInAllowedRange()
    {
        string baseUri = "http://sonarqube.test.de/";
        string projectKey = "project-key";
        string metrics = "ncloc,bugs,vulnerabilities";
        int pageSize = 123;
        SqComponentTreeUriBuilder uri = new SqComponentTreeUriBuilder(baseUri, projectKey, metrics);

        uri.PageSize(pageSize);
        uri.PageSize(501);

        string expectedUri = baseUri + "api/measures/component_tree"
           + "?baseComponentKey=" + projectKey + "&metricKeys="
           + metrics + "&" + "ps=" + pageSize;
        Assert.AreEqual(expectedUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void PageSizeIsOverridden()
    {
        string baseUri = "http://sonarqube.test.de/";
        string projectKey = "project-key";
        string metrics = "ncloc,bugs,vulnerabilities";
        int pageSize = 2;
        SqComponentTreeUriBuilder uri = new SqComponentTreeUriBuilder(baseUri, projectKey, metrics);

        uri.PageSize(1);
        uri.PageSize(pageSize);

        string expectedUri = baseUri + "api/measures/component_tree"
           + "?baseComponentKey=" + projectKey + "&metricKeys="
           + metrics + "&" + "ps=" + pageSize;
        Assert.AreEqual(expectedUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void PageAndPageSizeAreSet()
    {
        string baseUri = "http://sonarqube.test.de/";
        string projectKey = "project-key";
        string metrics = "ncloc,bugs,vulnerabilities";
        int pageSize = 1;
        int page = 1;
        SqComponentTreeUriBuilder uri = new SqComponentTreeUriBuilder(baseUri, projectKey, metrics);

        uri.Page(page);
        uri.PageSize(pageSize);

        string expectedUri = baseUri + "api/measures/component_tree"
           + "?baseComponentKey=" + projectKey + "&metricKeys="
           + metrics + "&" + "ps=" + page + "&" + "p=" + pageSize;
        Assert.AreEqual(expectedUri, uri.GetSqUri().ToString());
    }
    
    [Test]
    public void EmptyUsernameCredentials()
    {
        string baseUri = "http://sonarqube.test.de/";
        string projectKey = "project-key";
        string metrics = "ncloc,bugs,vulnerabilities";
        string username = "";
        string password = "password";
        SqComponentTreeUriBuilder uri = new SqComponentTreeUriBuilder(baseUri, projectKey, metrics);

        uri.UserCredentials(username, password);

        string expectedUri = baseUri + "api/measures/component_tree?baseComponentKey="
            + projectKey + "&metricKeys=" + metrics;
        Assert.AreEqual(expectedUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void EmptyPasswordCredentials()
    {
        string baseUri = "http://sonarqube.test.de/";
        string projectKey = "project-key";
        string metrics = "ncloc,bugs,vulnerabilities";
        string username = "username";
        string password = "";
        SqComponentTreeUriBuilder uri = new SqComponentTreeUriBuilder(baseUri, projectKey, metrics);

        uri.UserCredentials(username, password);

        string expectedUri = baseUri + "api/measures/component_tree?baseComponentKey="
            + projectKey + "&metricKeys=" + metrics;
        Assert.AreEqual(expectedUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void NullUsernameCredentials()
    {
        string baseUri = "http://sonarqube.test.de/";
        string projectKey = "project-key";
        string metrics = "ncloc,bugs,vulnerabilities";
        string username = null;
        string password = "password";
        SqComponentTreeUriBuilder uri = new SqComponentTreeUriBuilder(baseUri, projectKey, metrics);

        uri.UserCredentials(username, password);

        string expectedUri = baseUri + "api/measures/component_tree?baseComponentKey="
            + projectKey + "&metricKeys=" + metrics;
        Assert.AreEqual(expectedUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void NullPasswordCredentials()
    {
        string baseUri = "http://sonarqube.test.de/";
        string projectKey = "project-key";
        string metrics = "ncloc,bugs,vulnerabilities";
        string username = "username";
        string password = null;
        SqComponentTreeUriBuilder uri = new SqComponentTreeUriBuilder(baseUri, projectKey, metrics);

        uri.UserCredentials(username, password);

        string expectedUri = baseUri + "api/measures/component_tree?baseComponentKey="
            + projectKey + "&metricKeys=" + metrics;
        Assert.AreEqual(expectedUri, uri.GetSqUri().ToString());
    }

    [Test]
    public void CredentialsInsertedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string projectKey = "project-key";
        string metrics = "ncloc,bugs,vulnerabilities";
        string username = "username";
        string password = "password";
        SqComponentTreeUriBuilder uri = new SqComponentTreeUriBuilder(baseUri, projectKey, metrics);

        uri.UserCredentials(username, password);

        string expectedUri = "http://" + username + ":" + password + "@" + "sonarqube.test.de/"
            + "api/measures/component_tree?baseComponentKey="
            + projectKey + "&metricKeys=" + metrics;
        Assert.AreEqual(expectedUri, uri.GetSqUri().ToString());
    }
}

