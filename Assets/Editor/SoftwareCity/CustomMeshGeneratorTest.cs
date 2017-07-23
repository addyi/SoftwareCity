using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using SoftwareCity.Rendering.Utils;

public class CustomMeshGeneratorTest {

	[Test]
	public void GeneratePyramidTest() {
        GameObject testGameObject = new GameObject();
        testGameObject.AddComponent<CustomMeshGenerator>();

        Mesh testMesh = testGameObject.GetComponent<CustomMeshGenerator>().GeneratePyramid(0.5f);

        Vector3[] testMeshVertices = testMesh.vertices;

        //bottom
        Assert.Contains(new Vector3(0.5f, 0.0f, 0.5f), testMeshVertices);
        Assert.Contains(new Vector3(-0.5f, 0.0f, 0.5f), testMeshVertices);
        Assert.Contains(new Vector3(0.5f, 0.0f, -0.5f), testMeshVertices);
        Assert.Contains(new Vector3(-0.5f, 0.0f, -0.5f), testMeshVertices);

        //top
        Assert.Contains(new Vector3(0.25f, 1.0f, 0.25f), testMeshVertices);
        Assert.Contains(new Vector3(-0.25f, 1.0f, 0.25f), testMeshVertices);
        Assert.Contains(new Vector3(0.25f, 1.0f, -0.25f), testMeshVertices);
        Assert.Contains(new Vector3(-0.25f, 1.0f, -0.25f), testMeshVertices);
    }
}
