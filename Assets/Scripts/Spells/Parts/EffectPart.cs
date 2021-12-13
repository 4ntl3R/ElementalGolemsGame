using UnityEngine;

namespace Spells.Parts
{
    [System.Serializable]
    public class EffectPart : ISpellPart
    {
        public EffectPart(SpellPartInfo info, float baseManaCost, float multiplierManaCost, EffectEnum effectName)
        {
            _info = info;
            _baseManaCost = baseManaCost;
            _multiplierManaCost = multiplierManaCost;
            _effectName = effectName;
        }

        [SerializeField] private SpellPartInfo _info;
        [SerializeField] private float _baseManaCost;
        [SerializeField] private float _multiplierManaCost;
        [SerializeField] private EffectEnum _effectName;

        public SpellPartInfo Info
        {
            get => _info;
        }
        public float BaseManaCost
        {
            get => _baseManaCost;
        }
        public float MultiplierManaCost
        {
            get => _multiplierManaCost;
        }
        public EffectEnum effectName
        {
            get => _effectName;
        }
    }
}
