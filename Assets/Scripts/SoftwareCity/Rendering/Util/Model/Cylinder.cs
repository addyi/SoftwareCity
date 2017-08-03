using UnityEngine;

namespace SoftwareCity.Rendering.Utils.Models
{
    public class Cylinder : MonoBehaviour
    {
        /// <summary>
        /// Cylinder height.
        /// </summary>
        private readonly float cylinderHeight = 1f;

        /// <summary>
        /// Count of the cylinder sides.
        /// </summary>
        private readonly int cylinderSides = 6;

        /// <summary>
        /// Count vertices.
        /// </summary>
        private int vert;

        /// <summary>
        /// Count vertices cap.
        /// </summary>
        private int verticesCap;

        /// <summary>
        /// Count vertices sides.
        /// </summary>
        private int verticesSides;

        /// <summary>
        /// Create cylinder mesh with 6 sides.
        /// </summary>
        /// <returns></returns>
        public Mesh Mesh()
        {
            verticesCap = cylinderSides * 2 + 2;
            verticesSides = cylinderSides * 2 + 2;

            Mesh mesh = new Mesh();
            mesh.vertices = Vertices();
            mesh.normals = Normales();
            mesh.uv = UVs();
            mesh.triangles = Triangles();

            return mesh;
        }

        /// <summary>
        /// Create Vertices
        /// </summary>
        /// <returns></returns>
        private Vector3[] Vertices()
        {
            Vector3[] vertices = new Vector3[verticesCap * 2 + verticesSides * 2];
            vert = 14;

            // top
            int sideCounter = 0;
            while (vert < verticesCap * 2)
            {
                float radius = Mathf.PI * 2f * sideCounter / cylinderSides;
                float cos = Mathf.Cos(radius);
                float sin = Mathf.Sin(radius);
                vertices[vert] = new Vector3(0.0f, cylinderHeight, 0.0f);
                vertices[vert + 1] = new Vector3(cos, cylinderHeight, sin);
                vert += 2;

                sideCounter++;
            }

            // sides
            sideCounter = 0;
            while (vert < verticesCap * 2 + verticesSides)
            {
                float radius = Mathf.PI * 2f * sideCounter / cylinderSides;
                float cos = Mathf.Cos(radius);
                float sin = Mathf.Sin(radius);
                vertices[vert] = new Vector3(cos, cylinderHeight, sin);
                vertices[vert + 1] = new Vector3(cos, 0, sin);
                vert += 2;

                sideCounter++;
            }
            return vertices;
        }

        /// <summary>
        /// Create triangles.
        /// </summary>
        /// <returns></returns>
        private int[] Triangles()
        {
            int i = 0;
            int sideCounter = 6;
            int[] triangles = new int[cylinderSides * 24];

            // top
            while (sideCounter < cylinderSides * 2)
            {
                int current = sideCounter * 2 + 2;
                int next = sideCounter * 2 + 4;

                triangles[i++] = current;
                triangles[i++] = next;
                triangles[i++] = next + 1;

                triangles[i++] = current;
                triangles[i++] = next + 1;
                triangles[i++] = current + 1;

                sideCounter++;
            }

            // sides
            while (sideCounter < cylinderSides * 3)
            {
                int current = sideCounter * 2 + 4;
                int next = sideCounter * 2 + 6;

                triangles[i++] = current;
                triangles[i++] = next;
                triangles[i++] = next + 1;

                triangles[i++] = current;
                triangles[i++] = next + 1;
                triangles[i++] = current + 1;

                sideCounter++;
            }

            return triangles;
        }

        /// <summary>
        /// Create uvs.
        /// </summary>
        /// <returns></returns>
        private Vector2[] UVs()
        {
            Vector2[] uvs = new Vector2[verticesCap * 2 + verticesSides * 2];
            int sideCounter = 0;
            vert = 14;  //because we only render the top and not the bottom

            // top
            while (vert < verticesCap * 2)
            {
                float t = (float)(sideCounter++) / cylinderSides;
                uvs[vert++] = new Vector2(0f, t);
                uvs[vert++] = new Vector2(1f, t);
            }

            // sides
            sideCounter = 0;
            while (vert < verticesCap * 2 + verticesSides)
            {
                float t = (float)(sideCounter++) / cylinderSides;
                uvs[vert++] = new Vector2(t, 0f);
                uvs[vert++] = new Vector2(t, 1f);
            }

            return uvs;
        }

        /// <summary>
        /// Create normales.
        /// </summary>
        /// <returns></returns>
        private Vector3[] Normales()
        {
            Vector3[] normales = new Vector3[verticesCap * 2 + verticesSides * 2];
            int sideCounter = 0;
            vert = 14;  //because we only render the top and not the bottom

            // top
            while (vert < verticesCap * 2)
            {
                normales[vert++] = Vector3.up;
            }

            // sides
            while (vert < verticesCap * 2 + verticesSides)
            {
                float radius = Mathf.PI * 2f * sideCounter++ / cylinderSides;
                normales[vert] = new Vector3(Mathf.Cos(radius), 0f, Mathf.Sin(radius));
                normales[vert + 1] = normales[vert];
                vert += 2;

                sideCounter++;
            }
            return normales;
        }
    }
}