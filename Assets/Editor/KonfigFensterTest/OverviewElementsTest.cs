using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using ConfigurationWindow.DataStorage;

public class OverviewElementsTest
{

    [Test]
    public void IsEmptyTest()
    {
        OverviewElements.Initialize();
        List<string> temp = new List<string>();
        Assert.AreEqual(temp.Count == 0, OverviewElements.IsEmpty());
    }

    [Test]
    public void RemoveElementsTest()
    {
        OverviewElements.Initialize();
        List<string> temp = new List<string>();
        temp.Add("Metrics");
        temp.Add("Metric");
        temp.Add("Pyramid");
        temp.Add("LinesOfCode");
        temp.Add("Complexity");
        temp.Add("Vulnerabilty");
        temp.Add("Test 1 2 3");

        foreach(string s in temp)
        {
            OverviewElements.InsertElement(s);
        }
        Assert.AreEqual(temp.Remove(temp[0]), OverviewElements.RemoveElement(0));
        Assert.AreEqual(temp.Remove(temp[1]), OverviewElements.RemoveElement(1));
    }

    [Test]
    public void ClearListTest()
    {
        OverviewElements.Initialize();
        List<string> temp = new List<string>();
        temp.Add("Metric");
        temp.Add("Pyramid");
        temp.Add("LinesOfCode");
        temp.Add("Complexity");
        temp.Add("Vulnerabilty");
        temp.Add("Test 1 2 3");
        foreach(string s in temp)
        {
            OverviewElements.InsertElement(s);
        }
        OverviewElements.ClearList();
        temp.Clear();
        Assert.AreEqual(temp.Count == 0, OverviewElements.IsEmpty());
    }

    [Test]
    public void ListLengthTest()
    {
        List<string> temp = new List<string>();
        OverviewElements.Initialize();
        temp.Add("Metric");
        temp.Add("Pyramid");
        temp.Add("LinesOfCode");
        temp.Add("Complexity");
        temp.Add("Vulnerabilty");
        temp.Add("Test 1 2 3");
        foreach(string s in temp)
        {
            OverviewElements.InsertElement(s);
        }
        Assert.AreEqual(temp.Count, OverviewElements.Length());
    }

    [Test]
    public void InsertElementTest()
    {
        OverviewElements.Initialize();
        List<string> temp = new List<string>();
        temp.Add("Metric");
        temp.Add("Pyramid");
        temp.Add("LinesOfCode");
        temp.Add("Complexity");
        temp.Add("Vulnerabilty");
        temp.Add("Test 1 2 3");

        string resutl = "";


        foreach (string s in temp)
        {
            resutl += s + " ";
            OverviewElements.InsertElement(s);
        }

        Assert.AreEqual(resutl, OverviewElements.Print());
    }

    [Test]
    public void GetElementTest()
    {
        OverviewElements.Initialize();
        List<string> temp = new List<string>();
        temp.Add("foo temp var");
        OverviewElements.InsertElement("foo temp var");
        Assert.AreEqual(temp[0], OverviewElements.GetElement(0));
    }
}
