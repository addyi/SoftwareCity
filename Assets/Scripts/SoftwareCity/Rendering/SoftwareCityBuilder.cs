using System.Collections.Generic;
using UnityEngine;
using SoftwareCity.Envelope.Dimension;
using SoftwareCity.Rendering.Utils;

namespace SoftwareCity.Rendering
{
    public class SoftwareCityBuilder : MonoBehaviour {

        /// <summary>
        /// Border of the packages.
        /// </summary>
        private readonly Vector3 packageBorder = new Vector3(0.2f, 0.0f, 0.2f);
    
        /// <summary>
        /// Save a list with gameobject helpers for generating
        /// </summary>
        private List<GameObject> helperGameObjects = new List<GameObject>();
    
        /// <summary>
        /// Height of a package.
        /// </summary>
        private float levelHeight = 0.1f;

        /// <summary>
        /// The current package level.
        /// </summary>
        private int packageLevel;

        /// <summary>
        /// Shifting factor to place the packages.
        /// </summary>
        private float shiftingFactorYDirection = -0.01f;

        /// <summary>
        /// Save the height of the tallest building.
        /// </summary>
        private float maxDocumentHeight;

        private PackageColorizer packageColorizer;

        /// <summary>
        /// Method to build a new software city.
        /// </summary>
        /// <param name="root"></param>
        public void Build(SQPackage root)
        {
            packageLevel = 1;
            maxDocumentHeight = 0.0f;

            packageColorizer = GetComponent<PackageColorizer>();

            GameObject rootGameObject = TraverseTree(root, packageLevel);

            DeleteHelperGameObjects(helperGameObjects);

            this.gameObject.transform.parent.localScale = new Vector3(rootGameObject.transform.localScale.x * 0.1f + 0.05f, maxDocumentHeight * 0.1f + 0.05f, rootGameObject.transform.localScale.z * 0.1f + 0.05f);

            rootGameObject.transform.SetParent(this.gameObject.transform);
            rootGameObject.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x / 10f, this.gameObject.transform.localScale.y / 10f, this.gameObject.transform.localScale.z / 10f);

            this.gameObject.transform.parent.GetComponent<EnvelopeDimension>().UpdateDimensionPoints();
        }

        /// <summary>
        /// Method to traverse the object tree to building the city.
        /// </summary>
        /// <param name="treeObject"></param>
        /// <param name="packageLevel"></param>
        /// <returns></returns>
        private GameObject TraverseTree(ISQObject treeObject, int packageLevel)
        {
            if (treeObject is SQPackage)
            {
                List<GameObject> childs = new List<GameObject>();

                foreach (ISQObject child in ((SQPackage)treeObject).GetChilds())
                {
                    childs.Add(TraverseTree(child, packageLevel + 1));
                }

                List<GameObject> childDocuments = FilterDocuments(childs);
                List<GameObject> childPackages = FilterPackages(childs);
                if (childs.Count > 0)
                {
                    if (childPackages.Count > 0 && childDocuments.Count > 0)
                    {
                        CalculateChildPositions(childDocuments);

                        GameObject helper = ComponentProducer.GenerateHelper();

                        helperGameObjects.Add(helper);

                        helper.transform.localScale = CalculatePackageSize(childDocuments, helper);
                        SetPackageGameObjectAsParent(helper, childDocuments);

                        childPackages.Add(helper);
                        CalculateChildPositions(childPackages);
                    }
                    else
                    {
                        if (childs.Count > 0)
                            CalculateChildPositions(childs);  //--> WICHTIG !!!!!!!!!!!!!!!!!!!!
                    }
                }

                GameObject packageGameObject = ComponentProducer.GeneratePackage();
                
                packageGameObject.GetComponent<Renderer>().material.color = packageColorizer.PackageLevelColor(packageLevel);

                if (childPackages.Count > 0 && childDocuments.Count > 0)
                {
                    packageGameObject.transform.localScale = CalculatePackageSize(childPackages, packageGameObject);
                    SetPackageGameObjectAsParent(packageGameObject, childPackages);
                }
                else
                {
                    packageGameObject.transform.localScale = CalculatePackageSize(childs, packageGameObject);
                    SetPackageGameObjectAsParent(packageGameObject, childs);
                }

                return packageGameObject;
            }
            else
            {
                GameObject documentGameObject = ComponentProducer.GenerateDocument();

                if (documentGameObject.GetComponent<Renderer>().bounds.size.y > maxDocumentHeight)
                    maxDocumentHeight = documentGameObject.GetComponent<Renderer>().bounds.size.y;

                return documentGameObject;
            }
        }

        /// <summary>
        /// Method to add gameobjects as childs.
        /// </summary>
        /// <param name="packageGameObject"></param>
        /// <param name="childs"></param>
        private void SetPackageGameObjectAsParent(GameObject packageGameObject, List<GameObject> childs)
        {
            foreach (GameObject child in childs)
            {
                child.transform.SetParent(packageGameObject.transform);
            }
        }

        /// <summary>
        /// Calculate the package size.
        /// </summary>
        /// <param name="childs"></param>
        /// <param name="packageGameObject"></param>
        /// <returns></returns>
        private Vector3 CalculatePackageSize(List<GameObject> childs, GameObject packageGameObject)
        {
            Vector3 minPosition = new Vector3(0.0f, 0.0f, 0.0f);
            Vector3 maxPosition = new Vector3(0.0f, 0.0f, 0.0f);

            foreach (GameObject child in childs)
            {
                Bounds childBounds = child.GetComponent<Renderer>().bounds;

                if (minPosition.x > childBounds.min.x)
                    minPosition.x = childBounds.min.x;

                if (minPosition.z > childBounds.min.z)
                    minPosition.z = childBounds.min.z;

                if (maxPosition.x < childBounds.max.x)
                    maxPosition.x = childBounds.max.x;

                if (maxPosition.z < childBounds.max.z)
                    maxPosition.z = childBounds.max.z;
            }

            float horizontalDistance = CalculateDistance(new Vector3(minPosition.x, 0.0f, 0.0f), new Vector3(maxPosition.x, 0.0f, 0.0f));

            float verticalDistance = CalculateDistance(new Vector3(0.0f, 0.0f, minPosition.z), new Vector3(0.0f, 0.0f, maxPosition.z));

            packageGameObject.transform.position = (minPosition + maxPosition) / 2f;
            packageGameObject.transform.position = new Vector3(packageGameObject.transform.position.x, shiftingFactorYDirection * packageLevel, packageGameObject.transform.position.z);

            return new Vector3(horizontalDistance, levelHeight, verticalDistance) + packageBorder;
        }

        /// <summary>
        /// Calculate the positions of the child gameobjects.
        /// </summary>
        /// <param name="childs"></param>
        private void CalculateChildPositions(List<GameObject> childs)
        {
            int sign = 1;
            int sequenceNumber = 0;

            float displacementFactorWidth = FindOutDisplacementFactorWidth(childs);
            float displacementFactorDepth = FindOutDisplacementFactorDepth(childs);

            GameObject prevGameObject = childs[0];

            if (prevGameObject.GetComponent<Information>().GetSQObjectType().Equals("document"))
                prevGameObject.transform.position = new Vector3(0.0f, prevGameObject.transform.position.y, 0.0f);
            else
                prevGameObject.transform.position = new Vector3(0.0f, 0.0f, 0.0f);

            int childIndex = 1;
            for (int listIndex = 0; childIndex < childs.Count; listIndex++)
            {
                if ((listIndex + 1) % 2 == 0)
                {
                    sign = -sign;
                }
                else
                {
                    sequenceNumber++;
                }

                for (int loopIndexSeqNumber = 0; loopIndexSeqNumber < sequenceNumber; loopIndexSeqNumber++)
                {
                    if (childIndex < childs.Count)
                    {
                        if (listIndex % 2 == 0)
                        {
                            childs[childIndex].transform.position = prevGameObject.transform.position + new Vector3(sign * displacementFactorWidth, 0.0f, 0.0f);
                        }
                        else
                        {
                            childs[childIndex].transform.position = prevGameObject.transform.position + new Vector3(0.0f, 0.0f, sign * displacementFactorDepth);
                        }
                        prevGameObject = childs[childIndex];
                        childIndex++;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Get all documents from childs.
        /// </summary>
        /// <param name="childs"></param>
        /// <returns></returns>
        private List<GameObject> FilterDocuments(List<GameObject> childs)
        {
            List<GameObject> documents = new List<GameObject>();
            foreach (GameObject child in childs)
            {
                if (child.GetComponent<Information>().GetSQObjectType().Equals("document"))
                    documents.Add(child);
            }

            return documents;
        }

        /// <summary>
        /// Get all packages from childs.
        /// </summary>
        /// <param name="childs"></param>
        /// <returns></returns>
        private List<GameObject> FilterPackages(List<GameObject> childs)
        {
            List<GameObject> packages = new List<GameObject>();
            foreach (GameObject child in childs)
            {
                if (child.GetComponent<Information>().GetSQObjectType().Equals("package"))
                    packages.Add(child);
            }

            return packages;
        }
    
        /// <summary>
        /// Find displacement factor in x direction to calculate the positions.
        /// </summary>
        /// <param name="childs"></param>
        /// <returns></returns>
        private float FindOutDisplacementFactorWidth(List<GameObject> childs)
        {
            float displacementFactorWidth = 0.0f;
            float prevDisplacementFactorWidth = 0.0f;
            Vector3 childSize;

            string lastSQType = "";

            foreach (GameObject child in childs)
            {
                childSize = child.GetComponent<Renderer>().bounds.size;

                if (childSize.x >= displacementFactorWidth)
                {
                    lastSQType = child.GetComponent<Information>().GetSQObjectType();
                    prevDisplacementFactorWidth = displacementFactorWidth;
                    displacementFactorWidth = childSize.x;
                }
                else
                {
                    if (childSize.z >= prevDisplacementFactorWidth)
                        prevDisplacementFactorWidth = childSize.z;
                }
            }

            if (lastSQType.Equals("package"))
                return (displacementFactorWidth / 2) + (prevDisplacementFactorWidth / 2) + 0.1f;
            return displacementFactorWidth + 0.1f;
        }
    
        /// <summary>
        /// Find displacement factor in z direction to calculate the positions.
        /// </summary>
        /// <param name="childs"></param>
        /// <returns></returns>
        private float FindOutDisplacementFactorDepth(List<GameObject> childs)
        {
            float displacementFactorDepth = 0.0f;
            float prevDisplacementFactorDepth = 0.0f;

            Vector3 childSize;

            string lastSQType = "";

            foreach (GameObject child in childs)
            {
                childSize = child.GetComponent<Renderer>().bounds.size;

                if (childSize.z >= displacementFactorDepth)
                {
                    lastSQType = child.GetComponent<Information>().GetSQObjectType();
                    prevDisplacementFactorDepth = displacementFactorDepth;
                    displacementFactorDepth = childSize.z;
                }
                else
                {
                    if (childSize.z >= prevDisplacementFactorDepth)
                        prevDisplacementFactorDepth = childSize.z;
                }
            }

            if (lastSQType.Equals("package"))
                return (displacementFactorDepth / 2) + (prevDisplacementFactorDepth / 2) + 0.1f;
            return displacementFactorDepth + 0.1f;
        }
    
        /// <summary>
        /// Calculate the distance between two points.
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private float CalculateDistance(Vector3 startPoint, Vector3 endPoint)
        {
            return Vector3.Distance(startPoint, endPoint);
        }

        /// <summary>
        /// Delete all gameobject helpers.
        /// </summary>
        /// <param name="helperGameObjects"></param>
        private void DeleteHelperGameObjects(List<GameObject> helperGameObjects)
        {
            List<GameObject> childs;
            foreach (GameObject helperGameObject in helperGameObjects)
            {
                childs = new List<GameObject>();
                foreach (Transform child in helperGameObject.transform)
                {
                    childs.Add(child.gameObject);
                }

                foreach (GameObject child in childs)
                {
                    child.transform.SetParent(helperGameObject.transform.parent);
                }

                Destroy(helperGameObject);
            }
        }
    }
}
