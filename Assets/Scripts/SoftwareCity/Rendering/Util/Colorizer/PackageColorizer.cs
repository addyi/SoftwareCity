using UnityEngine;

namespace SoftwareCity.Rendering.Utils.Colorizer
{
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
}

