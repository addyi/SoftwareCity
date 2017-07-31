using NUnit.Framework;
using DataModel;
using DiskIO.AvailableMetrics;

public class ModelTest
{
    private readonly Model model = Model.GetInstance();

    [Test]
    public void ReadAvailableMetrics()
    {
        model.SetAvailableMetrics(AvailableMetricConfigReader.ReadConfigFile());

        Assert.GreaterOrEqual(model.GetAvailableMetrics().Count, 4);
    }

    [Test]
    public void AreSelectedMetricsConform()
    {
        // hat kein Sinn
        Metric[] randomMetrics = {
           new Metric("Lines of Code", "ncloc", 0.0, "double"),
           new Metric("Bugs", "bugs", 0.0, "double"),
           new Metric("Code Smells", "code_smells", 0.0, "double"),
           new Metric("Comment Lines Density", "comment_lines_density", 0.0, "percentage")
        };

        model.SetSelectedMetrics(randomMetrics);

        Assert.AreEqual(model.GetSelectedMetrics()[0], randomMetrics[0]);
        Assert.AreEqual(model.GetSelectedMetrics()[1], randomMetrics[1]);
        Assert.AreEqual(model.GetSelectedMetrics()[2], randomMetrics[2]);
        Assert.AreEqual(model.GetSelectedMetrics()[3], randomMetrics[3]);
    }

    [Test]
    public void CredentialTest()
    {
        this.model.SetCredentials("http://sonarqube.test.de", "user", "abcdef");

        Assert.AreEqual(this.model.GetBaseUrl(), "http://sonarqube.test.de");
        Assert.AreEqual(this.model.GetUsername(), "user");
        Assert.AreEqual(this.model.GetPassword(), "abcdef");
    }
}
