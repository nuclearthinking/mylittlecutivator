using System;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    public class ImageTransparency : MonoBehaviour
    {
        public float transparacy;

        private void Start()
        {
            var image = GetComponent<Image>();
            var color = image.color;
            color = new Color(color.r, color.g, color.b, transparacy);
            image.color = color;
        }
    }
}