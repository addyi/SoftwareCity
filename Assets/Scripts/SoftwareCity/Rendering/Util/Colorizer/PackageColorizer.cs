using UnityEngine;

namespace SoftwareCity.Rendering.Utils.Colorizer
{
    public class PackageColorizer : MonoBehaviour {

        /// <summary>
        /// Start package color.
        /// </summary>
        [SerializeField]
        private Color startColor;

        /// <summary>
        /// End package color.
        /// </summary>
        [SerializeField]
        private Color endColor;

        /// <summary>
        /// Current package color.
        /// </summary>
        [SerializeField]
        private Color currentColor;

        /// <summary>
        /// Lerp factor.
        /// </summary>
        [SerializeField]
        private float lerpFactor;

        /// <summary>
        /// Get the color of the specific package level.
        /// </summary>
        /// <param name="packageLevel"></param>
        /// <returns></returns>
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

