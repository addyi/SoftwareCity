using NUnit.Framework;
using Webservice.UriBuilding;
using System;

class SqProjectUriBuilderTest
{
    [Test]
    public void UriIsSetCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";

        SqProjectUriBuilder uri = new SqProjectUriBuilder(baseUri);

        Assert.AreEqual(baseUri + "api/projects/index", uri.GetSqUri().ToString());
    }
    
    [Test]
    public void BaseUriNotEmpty()
    {
        Assert.Throws<UriFormatException>(() => new SqProjectUriBuilder(""));
    }

    [Test]
    public void BaseUriNotNull()
    {
        Assert.Throws<ArgumentNullException>(() => new SqProjectUriBuilder(null));
    }

    [Test]
    public void NullPathIsAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string toAppend = "some/path/";
        string alsoToAppend = null;

        SqProjectUriBuilder uri = new SqProjectUriBuilder(baseUri);
        uri.AppendToPath(toAppend);
        uri.AppendToPath(alsoToAppend);

        Assert.AreEqual(baseUri + "api/projects/index/" + toAppend, uri.GetSqUri().ToString());
    }

    [Test]
    public void EmptyPathIsAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string toAppend = "some/path/";
        string alsoToAppend = "";

        SqProjectUriBuilder uri = new SqProjectUriBuilder(baseUri);
        uri.AppendToPath(toAppend);
        uri.AppendToPath(alsoToAppend);

        Assert.AreEqual(baseUri + "api/projects/index/" + toAppend, uri.GetSqUri().ToString());
    }

    [Test]
    public void IsPathAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string toAppend = "some/path/";

        SqProjectUriBuilder uri = new SqProjectUriBuilder(baseUri);
        uri.AppendToPath(toAppend);

        Assert.AreEqual(baseUri + "api/projects/index/" + toAppend, uri.GetSqUri().ToString());
    }

    [Test]
    public void AreMultiplePathsAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string toAppend = "some/path/";
        string alsoToAppend = "other/path/";

        SqProjectUriBuilder uri = new SqProjectUriBuilder(baseUri);
        uri.AppendToPath(toAppend);
        uri.AppendToPath(alsoToAppend);

        Assert.AreEqual(baseUri + "api/projects/index/" + toAppend + alsoToAppend, uri.GetSqUri().ToString());
    }

    [Test]
    public void IsQueryAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string query = "key=value";

        SqProjectUriBuilder uri = new SqProjectUriBuilder(baseUri);
        uri.AppendToQuery(query);

        Assert.AreEqual(baseUri + "api/projects/index" + "?" + query, uri.GetSqUri().ToString());
    }

    [Test]
    public void AreMultipleQuerysAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string query = "key=value";
        string alsoToAppend = "anotherKey=anotherValue";

        SqProjectUriBuilder uri = new SqProjectUriBuilder(baseUri);
        uri.AppendToQuery(query);
        uri.AppendToQuery(alsoToAppend);

        Assert.AreEqual(baseUri + "api/projects/index" + "?" + query + "&" + alsoToAppend, uri.GetSqUri().ToString());
    }

    [Test]
    public void NullQueryIsAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string query = "key=value";
        string alsoToAppend = null;

        SqProjectUriBuilder uri = new SqProjectUriBuilder(baseUri);
        uri.AppendToQuery(query);
        uri.AppendToQuery(alsoToAppend);

        Assert.AreEqual(baseUri + "api/projects/index" + "?" + query, uri.GetSqUri().ToString());
    }

    [Test]
    public void EmptyQueryIsAppendedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string query = "key=value";
        string alsoToAppend = "";

        SqProjectUriBuilder uri = new SqProjectUriBuilder(baseUri);
        uri.AppendToQuery(query);
        uri.AppendToQuery(alsoToAppend);

        Assert.AreEqual(baseUri + "api/projects/index" + "?" + query, uri.GetSqUri().ToString());
    }

    [Test]
    public void EmptyUsernameCredentials()
    {
        string baseUri = "http://sonarqube.test.de/";
        string username = "";
        string password = "password";

        SqProjectUriBuilder uri = new SqProjectUriBuilder(baseUri);
        uri.UserCredentials(username, password);

        Assert.AreEqual(baseUri + "api/projects/index", uri.GetSqUri().ToString());
    }

    [Test]
    public void EmptyPasswordCredentials()
    {
        string baseUri = "http://sonarqube.test.de/";
        string username = "username";
        string password = "";

        SqProjectUriBuilder uri = new SqProjectUriBuilder(baseUri);
        uri.UserCredentials(username, password);

        Assert.AreEqual(baseUri + "api/projects/index", uri.GetSqUri().ToString());
    }

    [Test]
    public void NullUsernameCredentials()
    {
        string baseUri = "http://sonarqube.test.de/";
        string username = null;
        string password = "password";

        SqProjectUriBuilder uri = new SqProjectUriBuilder(baseUri);
        uri.UserCredentials(username, password);

        Assert.AreEqual(baseUri + "api/projects/index", uri.GetSqUri().ToString());
    }

    [Test]
    public void NullPasswordCredentials()
    {
        string baseUri = "http://sonarqube.test.de/";
        string username = "username";
        string password = null;

        SqProjectUriBuilder uri = new SqProjectUriBuilder(baseUri);
        uri.UserCredentials(username, password);

        Assert.AreEqual(baseUri + "api/projects/index", uri.GetSqUri().ToString());
    }

    [Test]
    public void CredentialsInsertedCorrect()
    {
        string baseUri = "http://sonarqube.test.de/";
        string username = "username";
        string password = "password";

        SqProjectUriBuilder uri = new SqProjectUriBuilder(baseUri);
        uri.UserCredentials(username, password);

        Assert.AreEqual("http://" + username + ":" + password + "@" + "sonarqube.test.de/api/projects/index", uri.GetSqUri().ToString());
    }
}