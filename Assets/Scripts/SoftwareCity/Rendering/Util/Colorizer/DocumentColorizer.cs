using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoftwareCity.Rendering.Utils.Colorizer
{
    public class DocumentColorizer : MonoBehaviour {

        [SerializeField]
        private Color color;

        private float h = 0.65f;
        private float s = 1f;
        private float v = 1f;

        public Color DocumentColor(float value)
        {
            color = Color.HSVToRGB(h - (value/100f), s, v);
            return color;
        }
    }
}

