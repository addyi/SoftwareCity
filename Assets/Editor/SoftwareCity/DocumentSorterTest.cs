using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using SoftwareCity.Rendering.Utils;

public class DocumentSorterTest {

    private GameObject g1;
    private GameObject g2;
    private GameObject g3;
    private GameObject g4;
    private GameObject g5;

    [Test]
	public void SortingByHeightDescTest() {
        List<GameObject> testGameObjects = GetTestGameObjects();

        List<GameObject> sortedList = DocumentSorter.SortingByHeightDesc(testGameObjects);

        Assert.AreEqual(sortedList[0], g1);
        Assert.AreEqual(sortedList[1], g2);
        Assert.AreEqual(sortedList[2], g3);
        Assert.AreEqual(sortedList[3], g4);
        Assert.AreEqual(sortedList[4], g5);
    }

    [Test]
    public void SortingByHeightAscTest()
    {
        List<GameObject> testGameObjects = GetTestGameObjects();

        List<GameObject> sortedList = DocumentSorter.SortingByHeightAsc(testGameObjects);

        Assert.AreEqual(sortedList[0], g5);
        Assert.AreEqual(sortedList[1], g4);
        Assert.AreEqual(sortedList[2], g3);
        Assert.AreEqual(sortedList[3], g2);
        Assert.AreEqual(sortedList[4], g1);
    }

    [Test]
    public void SortingByWidthDescTest()
    {
        List<GameObject> testGameObjects = GetTestGameObjects();

        List<GameObject> sortedList = DocumentSorter.SortingByWidthDesc(testGameObjects);

        Assert.AreEqual(sortedList[0], g5);
        Assert.AreEqual(sortedList[1], g4);
        Assert.AreEqual(sortedList[2], g3);
        Assert.AreEqual(sortedList[3], g2);
        Assert.AreEqual(sortedList[4], g1);
    }

    [Test]
    public void SortingByWidthAscTest()
    {
        List<GameObject> testGameObjects = GetTestGameObjects();

        List<GameObject> sortedList = DocumentSorter.SortingByWidthAsc(testGameObjects);

        Assert.AreEqual(sortedList[0], g1);
        Assert.AreEqual(sortedList[1], g2);
        Assert.AreEqual(sortedList[2], g3);
        Assert.AreEqual(sortedList[3], g4);
        Assert.AreEqual(sortedList[4], g5);
    }

    [Test]
    public void SortingByHeightAndWidthDescTest()
    {
        List<GameObject> testGameObjects = GetTestGameObjects();

        List<GameObject> sortedList = DocumentSorter.SortingByHeightAndWidthDesc(testGameObjects);

        Assert.AreEqual(sortedList[0], g1);
        Assert.AreEqual(sortedList[1], g2);
        Assert.AreEqual(sortedList[2], g3);
        Assert.AreEqual(sortedList[3], g4);
        Assert.AreEqual(sortedList[4], g5);
    }

    [Test]
    public void SortingByHeightAndWidthAscTest()
    {
        List<GameObject> testGameObjects = GetTestGameObjects();

        List<GameObject> sortedList = DocumentSorter.SortingByHeightAndWidthAsc(testGameObjects);

        Assert.AreEqual(sortedList[0], g1);
        Assert.AreEqual(sortedList[1], g2);
        Assert.AreEqual(sortedList[2], g3);
        Assert.AreEqual(sortedList[3], g4);
        Assert.AreEqual(sortedList[4], g5);
    }

    private List<GameObject> GetTestGameObjects()
    {
        g1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        g1.transform.localScale = new Vector3(1.0f, 5.0f, 1.0f);

        g2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        g2.transform.localScale = new Vector3(2.0f, 4.0f, 2.0f);

        g3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        g3.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);

        g4 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        g4.transform.localScale = new Vector3(4.0f, 2.0f, 4.0f);

        g5 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        g5.transform.localScale = new Vector3(5.0f, 1.0f, 5.0f);

        List<GameObject> testGameObjects = new List<GameObject>();
        testGameObjects.Add(g1);
        testGameObjects.Add(g2);
        testGameObjects.Add(g3);
        testGameObjects.Add(g4);
        testGameObjects.Add(g5);

        return testGameObjects;
    }
}
