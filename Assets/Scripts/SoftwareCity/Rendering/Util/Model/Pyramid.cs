using UnityEngine;

namespace SoftwareCity.Rendering.Utils.Models
{
    public class Pyramid : MonoBehaviour {

        /// <summary>
        /// Vertice in direction bottom north west.
        /// </summary>
        private readonly Vector3 bottomNorthWest = new Vector3(-0.5f, 0, 0.5f);

        /// <summary>
        /// Vertice in direction bottom north east.
        /// </summary>
        private readonly Vector3 bottomNorthEast = new Vector3(0.5f, 0.0f, 0.5f);

        /// <summary>
        /// Vertice in direction bottom south east.
        /// </summary>
        private readonly Vector3 bottomSouthEast = new Vector3(0.5f, 0, -0.5f);

        /// <summary>
        /// Vertice in direction bottom south west.
        /// </summary>
        private readonly Vector3 bottomSoutWest = new Vector3(-0.5f, 0, -0.5f);

        /// <summary>
        /// Vertice in direction top north west.
        /// </summary>
        private readonly Vector3 topNorthWest = new Vector3(-0.5f, 1, 0.5f);

        /// <summary>
        /// Vertice in direction top north east.
        /// </summary>
        private readonly Vector3 topNorthEast = new Vector3(0.5f, 1, 0.5f);

        /// <summary>
        /// Vertice in direction top south east.
        /// </summary>
        private readonly Vector3 topSouthEast = new Vector3(0.5f, 1, -0.5f);

        /// <summary>
        /// Vertice in direction top south west.
        /// </summary>
        private readonly Vector3 topSouthWest = new Vector3(-0.5f, 1, -0.5f);

        /// <summary>
        /// Normals of the customer mesh.
        /// </summary>
        private readonly Vector3[] normals = new Vector3[]
        {
            //Bottom
            Vector3.down, Vector3.down, Vector3.down, Vector3.down,
            //Left
            Vector3.left, Vector3.left, Vector3.left, Vector3.left,
            //Front
            Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward,
            //Back
            Vector3.back, Vector3.back, Vector3.back, Vector3.back,
            //Right
            Vector3.right, Vector3.right, Vector3.right, Vector3.right,
            //Top
            Vector3.up, Vector3.up, Vector3.up, Vector3.up
        };

        /// <summary>
        /// Triangles of the custome mesh.
        /// </summary>
        private readonly int[] triangles = new int[]
        {
            // Bottom
	        3, 1, 0,
            3, 2, 1,			
	        // Left
	        7, 5, 4,
            7, 6, 5,
	        // Front
	        11, 9, 8,
            11, 10, 9,
	        // Back
	        15, 13, 12,
            15, 14, 13,
	        // Right
	        19, 17, 16,
            19, 18, 17,
	        // Top
	        23, 21, 20,
            23, 22, 21       
        };

        /// <summary>
        /// UVs of the custom mesh.
        /// </summary>
        private readonly Vector2[] uvs = new Vector2[]
        {
            //Bottom
            new Vector2(1.0f, 1.0f), new Vector2(0.0f, 1.0f), new Vector2(0.0f, 0.0f), new Vector2(1.0f, 0.0f),
            //Left
            new Vector2(1.0f, 1.0f), new Vector2(0.0f, 1.0f), new Vector2(0.0f, 0.0f), new Vector2(1.0f, 0.0f),
            //Front
            new Vector2(1.0f, 1.0f), new Vector2(0.0f, 1.0f), new Vector2(0.0f, 0.0f), new Vector2(1.0f, 0.0f),
            //Back
            new Vector2(1.0f, 1.0f), new Vector2(0.0f, 1.0f), new Vector2(0.0f, 0.0f), new Vector2(1.0f, 0.0f),
            //Right
            new Vector2(1.0f, 1.0f), new Vector2(0.0f, 1.0f), new Vector2(0.0f, 0.0f), new Vector2(1.0f, 0.0f),
            //Top
            new Vector2(1.0f, 1.0f), new Vector2(0.0f, 1.0f), new Vector2(0.0f, 0.0f), new Vector2(1.0f, 0.0f),
        };

        /// <summary>
        /// Method to generate a custom pyramid depend on the percent value.
        /// </summary>
        /// <param name="percent"></param>
        /// <returns></returns>
        public Mesh Mesh(float percent)
        {
            Mesh mesh = new Mesh();

            mesh.vertices = GetVerticies(percent);
            mesh.triangles = triangles;
            mesh.normals = normals;
            mesh.uv = uvs;

            return mesh;
        }

        /// <summary>
        /// Vertices of the custom mesh.
        /// </summary>
        /// <param name="percent"></param>
        /// <returns></returns>
        private Vector3[] GetVerticies(float percent)
        {
            Vector3 shiftingVector = new Vector3(percent, 1.0f, percent);

            Vector3 currentTopNorthWest = Vector3.Scale(topNorthWest, shiftingVector);
            Vector3 currentTopNorthEast = Vector3.Scale(topNorthEast, shiftingVector);
            Vector3 currentTopSouthEast = Vector3.Scale(topSouthEast, shiftingVector);
            Vector3 currentTopSouthWest = Vector3.Scale(topSouthWest, shiftingVector);

            return new Vector3[]
            {
                //Bottom
                bottomNorthWest, bottomNorthEast, bottomSouthEast, bottomSoutWest,
                //Left
                currentTopSouthWest, currentTopNorthWest, bottomNorthWest, bottomSoutWest,
                //Front
                currentTopNorthWest, currentTopNorthEast, bottomNorthEast, bottomNorthWest,
                //Back
                currentTopSouthEast, currentTopSouthWest, bottomSoutWest, bottomSouthEast,
                //Right
                currentTopNorthEast, currentTopSouthEast, bottomSouthEast, bottomNorthEast,
                //Top
                currentTopSouthWest, currentTopSouthEast, currentTopNorthEast, currentTopNorthWest
            };
        }
    }
}
