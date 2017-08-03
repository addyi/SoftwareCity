using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using SoftwareCity.Rendering.Utils;
using DataModel.ProjectTree.Components;
using Webservice.Response.ComponentTree;
using SoftwareCity.Rendering.Utils.Information;

public class ComponentLayoutTest {

	[Test]
	public void HelixTest() {
        List<GameObject> testGameObjects = new List<GameObject>();
        testGameObjects.Add(CreateTestGameObject());
        testGameObjects.Add(CreateTestGameObject());
        testGameObjects.Add(CreateTestGameObject());
        testGameObjects.Add(CreateTestGameObject());
        testGameObjects.Add(CreateTestGameObject());

        ComponentLayout.Helix(testGameObjects);

        Assert.AreEqual(testGameObjects[0].transform.position, new Vector3(0.0f, 0.0f, 0.0f));
        Assert.AreEqual(testGameObjects[1].transform.position, new Vector3(1.1f, 0.0f, 0.0f));
        Assert.AreEqual(testGameObjects[2].transform.position, new Vector3(1.1f, 0.0f, -1.1f));
        Assert.AreEqual(testGameObjects[3].transform.position, new Vector3(0.0f, 0.0f, -1.1f));
        Assert.AreEqual(testGameObjects[4].transform.position, new Vector3(-1.1f, 0.0f, -1.1f));
    }

    [Test]
    public void CornerTest()
    {
        List<GameObject> testGameObjects = new List<GameObject>();
        testGameObjects.Add(CreateTestGameObject());
        testGameObjects.Add(CreateTestGameObject());
        testGameObjects.Add(CreateTestGameObject());
        testGameObjects.Add(CreateTestGameObject());
        testGameObjects.Add(CreateTestGameObject());

        ComponentLayout.Corner(testGameObjects);

        Assert.AreEqual(testGameObjects[0].transform.position, new Vector3(0.0f, 0.0f, 0.0f));
        Assert.AreEqual(testGameObjects[1].transform.position, new Vector3(1.1f, 0.0f, 0.0f));
        Assert.AreEqual(testGameObjects[2].transform.position, new Vector3(0.0f, 0.0f, -1.1f));
        Assert.AreEqual(testGameObjects[3].transform.position, new Vector3(2.2f, 0.0f, 0.0f));
        Assert.AreEqual(testGameObjects[4].transform.position, new Vector3(1.1f, 0.0f, -1.1f));
    }

    [Test]
    public void FindOutDisplacementFactorWidthTest()
    {
        List<GameObject> testGameObjects = new List<GameObject>();
        testGameObjects.Add(CreateTestGameObject());
        testGameObjects.Add(CreateTestGameObject());
        testGameObjects.Add(CreateTestGameObject());
        testGameObjects.Add(CreateTestGameObject());
        testGameObjects.Add(CreateTestGameObject());

        Assert.AreEqual(ComponentLayout.FindOutDisplacementFactorWidth(testGameObjects), 1.1f);
    }
    
    [Test]
    public void FindOutDisplacementFactorDepth()
    {
        List<GameObject> testGameObjects = new List<GameObject>();
        testGameObjects.Add(CreateTestGameObject());
        testGameObjects.Add(CreateTestGameObject());
        testGameObjects.Add(CreateTestGameObject());
        testGameObjects.Add(CreateTestGameObject());
        testGameObjects.Add(CreateTestGameObject());

        Assert.AreEqual(ComponentLayout.FindOutDisplacementFactorDepth(testGameObjects), 1.1f);
    }

    private GameObject CreateTestGameObject()
    {
        GameObject g = GameObject.CreatePrimitive(PrimitiveType.Cube);
        g.AddComponent<BaseInformation>();
        g.GetComponent<BaseInformation>().UpdateValues(GetDirectoryTreeComponent());
        return g;
    }

    private DirComponent GetDirectoryTreeComponent()
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
}
