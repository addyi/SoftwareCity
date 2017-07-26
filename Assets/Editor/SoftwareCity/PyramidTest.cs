using UnityEngine;
using NUnit.Framework;
using SoftwareCity.Rendering.Utils.Models;

public class PyramidTest {

	[Test]
	public void Mesh() {
        GameObject testGameObject = new GameObject();
        testGameObject.AddComponent<Pyramid>();

        Mesh testMesh = testGameObject.GetComponent<Pyramid>().Mesh(0.5f);

        Vector3[] testMeshVertices = testMesh.vertices;

        Assert.AreEqual(testMesh.vertices.Length, 24);
        Assert.AreEqual(testMesh.normals.Length, 24);
        Assert.AreEqual(testMesh.uv.Length, 24);
        Assert.AreEqual(testMesh.triangles.Length, 36);

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
