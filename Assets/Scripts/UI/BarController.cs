using System;
using System.Collections.Generic;
using Base;
using Positions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public struct BarProperties
    {
        public Slider slider { get; }
        public TextMeshProUGUI text { get; }

        public BarProperties(Slider slider, TextMeshProUGUI text)
        {
            this.slider = slider;
            this.text = text;
        }
        

        public static BarProperties FindProperties(GameObject gameObject)
        {
            return new BarProperties(gameObject.GetComponentInChildren<Slider>(), gameObject.GetComponentInChildren<TextMeshProUGUI>());
        }
    }
    
    public class BarController : MonoBehaviour
    {
        [SerializeField]
        private GameObject healthBar;
        [SerializeField]
        private GameObject manaBar;
        [SerializeField]
        private GameObject staminaBar;


        private GolemBase _golemBase;
        private Dictionary<GameObject, BarProperties> _propertiesMap;

        private void Awake()
        {
            _propertiesMap = new Dictionary<GameObject, BarProperties>();
            _propertiesMap[healthBar] = BarProperties.FindProperties(healthBar);
            _propertiesMap[manaBar] = BarProperties.FindProperties(manaBar);
            _propertiesMap[staminaBar] = BarProperties.FindProperties(staminaBar);
            
            _golemBase = GetComponentInParent<GolemBase>();
            _golemBase.initiative.OnValueChange += OnStaminaChange;
            _golemBase.magic.OnValueChange += OnManaChange;
            _golemBase.damage.OnValueChange += OnHealthChange;
        }

        private void OnStaminaChange(float current, float max)
        {   
            ChangeValues(current, max, _propertiesMap[staminaBar]);
        }
        
        private void OnManaChange(float current, float max)
        {
            ChangeValues(current, max, _propertiesMap[manaBar]);
        }
        
        private void OnHealthChange(float current, float max)
        {
            ChangeValues(current, max, _propertiesMap[healthBar]);
        }

        private void ChangeValues(float current, float max, BarProperties properties)
        {
            properties.slider.value = current / max;
            properties.text.text = current + "/" + max;
        }
    }
}
