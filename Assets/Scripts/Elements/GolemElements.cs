using System;
using UnityEngine;

namespace Elements
{
    [Serializable]
    public class GolemElements
    {
        [SerializeField]
        private ElementsType elementType;
        public event Action<ElementsType> OnElementsChange;


        public void Init()
        {
            OnElementsChange?.Invoke(elementType);
        }
        
        public void ChangeType(ElementsType newType)
        {
            elementType = newType;
            OnElementsChange?.Invoke(elementType);
        }
        
        public void ChangeType()
        {
            switch (elementType)
            {
                case ElementsType.Earth:
                    ChangeType(ElementsType.Water);
                    break;
                case ElementsType.Water:
                    ChangeType(ElementsType.Fire);
                    break;
                case ElementsType.Fire:
                    ChangeType(ElementsType.Earth);
                    break;
            }
        }
    }
}
