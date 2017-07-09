using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoftwareCity.Rendering.Utils
{
    public class ComponentLayout : MonoBehaviour
    {

        /// <summary>
        /// Calculate the positions of the child gameobjects.
        /// </summary>
        /// <param name="childs"></param>
        public static void CalculateChildPositions(List<GameObject> childs)
        {
            int sign = 1;
            int sequenceNumber = 0;

            //childs = DocumentSorter.SortingByHeightDesc(childs);
            //childs = DocumentSorter.SortingByWidthDesc(childs);
            childs = DocumentSorter.SortingByHeightAndWidthDesc(childs);

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
        /// Find displacement factor in x direction to calculate the positions.
        /// </summary>
        /// <param name="childs"></param>
        /// <returns></returns>
        private static float FindOutDisplacementFactorWidth(List<GameObject> childs)
        {
            float displacementFactorWidth = 0.0f;
            float prevDisplacementFactorWidth = 0.0f;
            Vector3 childSize;

            string lastSQType = "";

            foreach (GameObject child in childs)
            {
                childSize = child.GetComponentInChildren<Renderer>().bounds.size;

                if (childSize.x >= displacementFactorWidth)
                {
                    lastSQType = child.GetComponent<Information>().GetSQObjectType();
                    prevDisplacementFactorWidth = displacementFactorWidth;
                    displacementFactorWidth = childSize.x;
                }
                else
                {
                    if (childSize.z >= prevDisplacementFactorWidth)
                        prevDisplacementFactorWidth = childSize.x;
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
        private static float FindOutDisplacementFactorDepth(List<GameObject> childs)
        {
            float displacementFactorDepth = 0.0f;
            float prevDisplacementFactorDepth = 0.0f;

            Vector3 childSize;

            string lastSQType = "";

            foreach (GameObject child in childs)
            {
                childSize = child.GetComponentInChildren<Renderer>().bounds.size;

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
                return (displacementFactorDepth * 0.5f) + (prevDisplacementFactorDepth * 0.5f) + 0.1f;
            return displacementFactorDepth + 0.1f;
        }
    }
}
