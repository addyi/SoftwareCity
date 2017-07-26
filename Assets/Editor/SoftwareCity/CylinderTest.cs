using UnityEngine;
using NUnit.Framework;
using SoftwareCity.Rendering.Utils.Models;

public class CylinderTest {

	[Test]
	public void MeshTest() {
        GameObject testGameObject = new GameObject();
        testGameObject.AddComponent<Cylinder>();

        Mesh testMesh = testGameObject.GetComponent<Cylinder>().Mesh();

        Vector3[] testMeshVertices = testMesh.vertices;

        Assert.AreEqual(testMesh.vertices.Length, 56);
        Assert.AreEqual(testMesh.normals.Length, 56);
        Assert.AreEqual(testMesh.uv.Length, 56);
        Assert.AreEqual(testMesh.triangles.Length, 144);
    }
}
