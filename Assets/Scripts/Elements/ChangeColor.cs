using System;
using System.Collections.Generic;
using Positions;
using UnityEngine;

namespace Elements
{
    [System.Serializable]
    public struct ElementColors
    {
        public ElementsType elementType;
        public Color color;

        public ElementColors(ElementsType elementType, Color color)
        {
            this.color = color;
            this.elementType = elementType;
        }
    }
    
    public class ChangeColor : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField] private List<ElementColors> _colorsList;

        private void Awake()
        {
            GetComponent<GolemBase>().elements.OnElementsChange += ColorChanger;
        }

        private void ColorChanger(ElementsType newType)
        {
            spriteRenderer.color = _colorsList.Find(x => x.elementType == newType).color;
        }
    }
}
