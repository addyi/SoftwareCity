using DataModel;
using SoftwareCity.Rendering.Utils.Information;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoftwareCity.Rendering.Utils
{
    public class ComponentLayout : MonoBehaviour
    {

        /// <summary>
        /// Calculate the positions of the child gameobjects in a helix.
        /// </summary>
        /// <param name="childs"></param>
        public static void Helix(List<GameObject> childs)
        {
            int sign = 1;
            int sequenceNumber = 0;

            //childs = DocumentSorter.SortingByHeightDesc(childs);
            //childs = DocumentSorter.SortingByWidthDesc(childs);
            childs = DocumentSorter.SortingByHeightAndWidthDesc(childs);

            float displacementFactorWidth = FindOutDisplacementFactorWidth(childs);
            float displacementFactorDepth = FindOutDisplacementFactorDepth(childs);

            GameObject prevGameObject = childs[0];
            
            if (prevGameObject.GetComponent<BaseInformation>().GetQualifier() == SqQualifier.FILE 
                || prevGameObject.GetComponent<BaseInformation>().GetQualifier() == SqQualifier.UNIT_TEST)
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
        /// Calculate the positions of the child gameobjects in a corner.
        /// </summary>
        /// <param name="childs"></param>
        public static void Corner(List<GameObject> childs)
        {
            childs = DocumentSorter.SortingByHeightDesc(childs);

            float displacementFactorWidth = FindOutDisplacementFactorWidth(childs);
            float displacementFactorDepth = FindOutDisplacementFactorDepth(childs);

            int indexList = 0;
            int i = 0;

            while(indexList < childs.Count)
            {
                int x = i;

                for(int y = 0; y >= -i && indexList < childs.Count; y--, x--, indexList++)
                {
                    childs[indexList].transform.position = new Vector3(x * displacementFactorWidth, 0.0f, y * displacementFactorDepth);
                }
                i++;
            }

        }

        /// <summary>
        /// Find displacement factor in x direction to calculate the positions.
        /// </summary>
        /// <param name="childs"></param>
        /// <returns></returns>
        public static float FindOutDisplacementFactorWidth(List<GameObject> childs)
        {
            float displacementFactorWidth = 0.0f;
            float prevDisplacementFactorWidth = 0.0f;
            Vector3 childSize;

            SqQualifier lastSQQualifier = SqQualifier.DIRECTORY;

            foreach (GameObject child in childs)
            {
                childSize = child.GetComponentInChildren<Renderer>().bounds.size;

                if (childSize.x >= displacementFactorWidth)
                {
                    lastSQQualifier = child.GetComponent<BaseInformation>().GetQualifier();
                    prevDisplacementFactorWidth = displacementFactorWidth;
                    displacementFactorWidth = childSize.x;
                }
                else
                {
                    if (childSize.z >= prevDisplacementFactorWidth)
                        prevDisplacementFactorWidth = childSize.x;
                }
            }

            if (SqQualifier.DIRECTORY == lastSQQualifier || SqQualifier.PROJECT == lastSQQualifier || SqQualifier.SUB_PROJECT == lastSQQualifier || SqQualifier.UNDEFINED == lastSQQualifier)
                return (displacementFactorWidth / 2) + (prevDisplacementFactorWidth / 2) + 0.1f;
            return displacementFactorWidth + 0.1f;
        }

        /// <summary>
        /// Find displacement factor in z direction to calculate the positions.
        /// </summary>
        /// <param name="childs"></param>
        /// <returns></returns>
        public static float FindOutDisplacementFactorDepth(List<GameObject> childs)
        {
            float displacementFactorDepth = 0.0f;
            float prevDisplacementFactorDepth = 0.0f;

            Vector3 childSize;

            SqQualifier lastSQQualifier = SqQualifier.DIRECTORY;

            foreach (GameObject child in childs)
            {
                childSize = child.GetComponentInChildren<Renderer>().bounds.size;

                if (childSize.z >= displacementFactorDepth)
                {
                    lastSQQualifier = child.GetComponent<BaseInformation>().GetQualifier();
                    prevDisplacementFactorDepth = displacementFactorDepth;
                    displacementFactorDepth = childSize.z;
                }
                else
                {
                    if (childSize.z >= prevDisplacementFactorDepth)
                        prevDisplacementFactorDepth = childSize.z;
                }
            }

            if (SqQualifier.DIRECTORY == lastSQQualifier || SqQualifier.PROJECT == lastSQQualifier || SqQualifier.SUB_PROJECT == lastSQQualifier || SqQualifier.UNDEFINED == lastSQQualifier)
                return (displacementFactorDepth * 0.5f) + (prevDisplacementFactorDepth * 0.5f) + 0.1f;
            return displacementFactorDepth + 0.1f;
        }
    }
}
