using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using SoftwareCity.Rendering.Utils.Information;
using DataModel.ProjectTree.Components;
using Webservice.Response.ComponentTree;
using DataModel;
using SoftwareCity.Rendering.Utils;

public class ComponentFilterTest {

	[Test]
	public void FilterDocumentsTest() {
        List<GameObject> testComponents = new List<GameObject>();

        int countDocuments = 5;
        int countDirectories = 3;

        for(int i=0;i<countDocuments; i++)
        {
            GameObject g = new GameObject();
            g.AddComponent<BaseInformation>();
            g.GetComponent<BaseInformation>().UpdateValues(GetDocumentTreeComponent());
            testComponents.Add(g);
        }

        for (int i = 0; i < countDirectories; i++)
        {
            GameObject g = new GameObject();
            g.AddComponent<BaseInformation>();
            g.GetComponent<BaseInformation>().UpdateValues(GetDirectoryTreeComponent());
            testComponents.Add(g);
        }

        Assert.AreEqual(ComponentFilter.FilterDocuments(testComponents).Count, countDocuments);
    }

    [Test]
    public void FilterPackagesTest()
    {
        List<GameObject> testComponents = new List<GameObject>();

        int countDocuments = 20;
        int countDirectories = 7;

        for (int i = 0; i < countDocuments; i++)
        {
            GameObject g = new GameObject();
            g.AddComponent<BaseInformation>();
            g.GetComponent<BaseInformation>().UpdateValues(GetDocumentTreeComponent());
            testComponents.Add(g);
        }

        for (int i = 0; i < countDirectories; i++)
        {
            GameObject g = new GameObject();
            g.AddComponent<BaseInformation>();
            g.GetComponent<BaseInformation>().UpdateValues(GetDirectoryTreeComponent());
            testComponents.Add(g);
        }

        Assert.AreEqual(ComponentFilter.FilterPackages(testComponents).Count, countDirectories);
    }

    private FilComponent GetDocumentTreeComponent()
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
