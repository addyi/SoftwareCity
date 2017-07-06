using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageColorizer : MonoBehaviour {

    [SerializeField]
    private Color startColor;

    [SerializeField]
    private Color endColor;

    [SerializeField]
    private Color currentColor;

    [SerializeField]
    private float lerpFactor;

	public Color PackageLevelColor(int packageLevel)
    {
        currentColor = endColor;
        for(int counter = 0; counter < packageLevel; counter++)
        {
            currentColor = Color.Lerp(currentColor, startColor, lerpFactor);
        }
        return currentColor;
    }
}
