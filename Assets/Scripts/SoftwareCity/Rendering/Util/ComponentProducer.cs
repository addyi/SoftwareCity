using DataModel.Metrics;
using DataModel.ProjectTree.Components;
using DiskIO.AvailableMetrics;
using SoftwareCity.Rendering.Utils.Colorizer;
using SoftwareCity.Rendering.Utils.Information;
using SoftwareCity.Rendering.Utils.Models;
using UnityEngine;
using UnityEngine.Rendering;

namespace SoftwareCity.Rendering.Utils {
    public class ComponentProducer : MonoBehaviour {

        [SerializeField]
        private GameObject documentPrefab;

        [SerializeField]
        private Material contentMaterial;

        private readonly float maxEnviromentHeight = 5.0f;

        /// <summary>
        /// Create a new document gameobject with the specific informations.
        /// </summary>
        /// <returns></returns>
        public GameObject GenerateDocument(TreeComponent documentComponent, Metric[] selectedMetrics, float maxHeight)
        {
            GameObject documentGameObject = Instantiate(documentPrefab) as GameObject;
            documentGameObject.AddComponent<FileInformation>();
            documentGameObject.GetComponent<FileInformation>().UpdateValues(documentComponent);
            documentGameObject.AddComponent<ComponentClickListener>();
            documentGameObject.GetComponent<Renderer>().material = contentMaterial;
            documentGameObject.GetComponent<Renderer>().shadowCastingMode = ShadowCastingMode.Off;
            documentGameObject.GetComponent<Renderer>().lightProbeUsage = LightProbeUsage.Off;
            documentGameObject.GetComponent<Renderer>().reflectionProbeUsage = ReflectionProbeUsage.Off;
            documentGameObject.GetComponent<Renderer>().receiveShadows = false;
            documentGameObject.GetComponent<Collider>().enabled = false;
            documentGameObject.GetComponent<Renderer>().enabled = false;
            documentGameObject.transform.position = Vector3.zero;
            documentGameObject.name = documentComponent.Name;

            if(((FilComponent)documentComponent).Language.Equals("xml"))
            {
                float widthDepth = FindSpecificMetricValue(selectedMetrics[0], documentComponent);
                documentGameObject.transform.localScale = CalculateDocumentSize(widthDepth, 0.0f, maxHeight);
                documentGameObject.GetComponent<Renderer>().material.color = Color.gray;
                documentGameObject.GetComponent<MeshFilter>().mesh = this.gameObject.GetComponent<Cylinder>().Mesh();

            } else
            {
                float defaultMetric = FindSpecificMetricValue(selectedMetrics[0], documentComponent);
                float heightMetric = FindSpecificMetricValue(selectedMetrics[1], documentComponent);
                documentGameObject.transform.localScale = CalculateDocumentSize(defaultMetric, heightMetric, maxHeight);

                float colorMetric = FindSpecificMetricValue(selectedMetrics[2], documentComponent);
                documentGameObject.GetComponent<Renderer>().material.color = GetComponent<DocumentColorizer>().DocumentColor(colorMetric);

                float pyramidMetric = FindSpecificMetricValue(selectedMetrics[3], documentComponent);
                documentGameObject.GetComponent<MeshFilter>().mesh = CalculatePyramid(pyramidMetric);
            }


            return documentGameObject;
        }

        /// <summary>
        /// Calculate the positions of the pyramid corners at the top.
        /// </summary>
        /// <param name="documentGameObject"></param>
        private Mesh CalculatePyramid(float percent)
        {
            return this.gameObject.GetComponent<Pyramid>().Mesh(percent / 100.0f);
        }

        /// <summary>
        /// Calculate the specific size depend on the the metric.
        /// </summary>
        /// <returns></returns>
        private Vector3 CalculateDocumentSize(float widthDepth, float height, float maxHeight)
        {
            float defaultHeight = 0.5f;
           // if(maxHeight > 0.0f)
           // {
                float currentDocumentHeight = (height+defaultHeight) / (maxHeight+defaultHeight);
                return new Vector3((0.1f + widthDepth)/100.0f, currentDocumentHeight * maxEnviromentHeight, (0.1f + widthDepth) / 100.0f);
           // }
           // return new Vector3((0.1f + widthDepth) / 100.0f, 0.1f * maxEnviromentHeight, (0.1f + widthDepth) / 100.0f);
        }

        /// <summary>
        /// Create a new package gameobject with the specific informations.
        /// </summary>
        /// <returns></returns>
        public GameObject GeneratePackage(TreeComponent packageComponent)
        {
            GameObject packageGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            packageGameObject.AddComponent<DirectoryInformation>();
            packageGameObject.GetComponent<DirectoryInformation>().UpdateValues(packageComponent);
            packageGameObject.AddComponent<ComponentClickListener>();
            packageGameObject.GetComponentInChildren<Renderer>().sharedMaterial = contentMaterial;
            packageGameObject.GetComponentInChildren<Renderer>().shadowCastingMode = ShadowCastingMode.Off;
            packageGameObject.GetComponentInChildren<Renderer>().lightProbeUsage = LightProbeUsage.Off;
            packageGameObject.GetComponentInChildren<Renderer>().reflectionProbeUsage = ReflectionProbeUsage.Off;
            packageGameObject.GetComponentInChildren<Renderer>().receiveShadows = false;
            packageGameObject.GetComponent<Collider>().enabled = false;
            packageGameObject.GetComponent<Renderer>().enabled = false;
            packageGameObject.name = packageComponent.Name;

            return packageGameObject;
        }

        /// <summary>
        /// Create a new package gameobject without informations.
        /// </summary>
        /// <returns></returns>
        public GameObject GenerateHelper()
        {
            GameObject helperGameobject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            helperGameobject.AddComponent<BaseInformation>();
            helperGameobject.name = "Helper";

            return helperGameobject;
        }

        private float FindSpecificMetricValue(Metric metric, TreeComponent documentComponent)
        {
            foreach (TreeMetric m in documentComponent.Metrics)
            {
                if (m.Key.Equals(metric.key))
                {
                    return (float)m.Value;
                }
            }
            return (float)metric.defaultValue;
        }

        public void SetDocumentPrefab(GameObject documentPrefab)
        {
            this.documentPrefab = documentPrefab;
        }
    }
}

