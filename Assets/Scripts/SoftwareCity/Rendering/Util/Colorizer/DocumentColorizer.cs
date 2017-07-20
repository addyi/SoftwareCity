using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoftwareCity.Rendering.Utils.Colorizer
{
    public class DocumentColorizer : MonoBehaviour {

        [SerializeField]
        private Color color;

        /// <summary>
        /// Hue.
        /// </summary>
        private float h = 0.65f;

        /// <summary>
        /// Saturation.
        /// </summary>
        private float s = 1f;

        /// <summary>
        /// Value.
        /// </summary>
        private float v = 1f;

        /// <summary>
        /// Create document color.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Color DocumentColor(float value)
        {
            color = Color.HSVToRGB(h - (value/100f), s, v);
            return color;
        }
    }
}

