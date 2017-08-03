using DataModel.ProjectTree.Components;
using NUnit.Framework;
using SoftwareCity.Rendering.Utils;
using System.Collections.Generic;
using Webservice.Response.ComponentTree;
using UnityEngine;
using UnityEngine.Rendering;

public class ComponentProducerTest {

	[Test]
	public void GenerateDocumentTest() {
        /*
        GameObject componentProducerTestGameObject = new GameObject();
        componentProducerTestGameObject.AddComponent<ComponentProducer>();
        componentProducerTestGameObject.GetComponent<ComponentProducer>().SetDocumentPrefab(Resources.Load("Document", typeof(GameObject))as GameObject);

        GameObject testGameObject = componentProducerTestGameObject.GetComponent<ComponentProducer>().GenerateDocument(GetFilTreeComponent());

        Assert.AreEqual(testGameObject.GetComponent<Renderer>().shadowCastingMode, ShadowCastingMode.Off);
        Assert.AreEqual(testGameObject.GetComponent<Renderer>().lightProbeUsage, LightProbeUsage.Off);
        Assert.AreEqual(testGameObject.GetComponent<Renderer>().reflectionProbeUsage, ReflectionProbeUsage.Off);
        Assert.False(testGameObject.GetComponent<Renderer>().receiveShadows);
        Assert.False(testGameObject.GetComponent<Collider>().enabled);
        Assert.False(testGameObject.GetComponent<Renderer>().enabled);
        Assert.AreEqual(testGameObject.transform.position, Vector3.zero);
        */
    }

    [Test]
    public void GeneratePackageTest()
    {
        GameObject componentProducerTestGameObject = new GameObject();
        componentProducerTestGameObject.AddComponent<ComponentProducer>();

        GameObject testGameObject = componentProducerTestGameObject.GetComponent<ComponentProducer>().GeneratePackage(GetDirTreeComponent());

        Assert.AreEqual(testGameObject.GetComponent<Renderer>().shadowCastingMode, ShadowCastingMode.Off);
        Assert.AreEqual(testGameObject.GetComponent<Renderer>().lightProbeUsage, LightProbeUsage.Off);
        Assert.AreEqual(testGameObject.GetComponent<Renderer>().reflectionProbeUsage, ReflectionProbeUsage.Off);
        Assert.False(testGameObject.GetComponent<Renderer>().receiveShadows);
        Assert.False(testGameObject.GetComponent<Collider>().enabled);
        Assert.False(testGameObject.GetComponent<Renderer>().enabled);
        Assert.AreEqual(testGameObject.transform.position, Vector3.zero);
    }

    [Test]
    public void GenerateHelperTest()
    {
        GameObject componentProducerTestGameObject = new GameObject();
        componentProducerTestGameObject.AddComponent<ComponentProducer>();

        GameObject testGameObject = componentProducerTestGameObject.GetComponent<ComponentProducer>().GenerateHelper();

        Assert.AreEqual(testGameObject.name, "Helper");
    }

    private DirComponent GetDirTreeComponent()
    {
        SqComponent sqComponent = new SqComponent();
        sqComponent.id = "id";
        sqComponent.key = "key";
        sqComponent.name = "name";
        sqComponent.qualifier = "DIR";
        sqComponent.path = "path";
        sqComponent.language = "language";

        sqComponent.measures = new List<Measure>();

        return new DirComponent(sqComponent);
    }

    private FilComponent GetFilTreeComponent()
    {
        SqComponent sqComponent = new SqComponent();
        sqComponent.id = "id";
        sqComponent.key = "key";
        sqComponent.name = "name";
        sqComponent.qualifier = "FIL";
        sqComponent.path = "path";
        sqComponent.language = "language";

        sqComponent.measures = new List<Measure>();

        return new FilComponent(sqComponent);
    }
}
