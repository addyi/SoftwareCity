using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections.Generic;

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
		Debug.Log(model.GetSelectedMetrics().Length);
		Assert.AreEqual(model.GetSelectedMetrics()[0].key, "ncloc");
		Assert.Equals(model.GetSelectedMetrics()[1].datatype, "double");
		Assert.Equals(model.GetSelectedMetrics()[2].key, "double");
		Assert.Equals(model.GetSelectedMetrics()[3].key, "percentage");

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
