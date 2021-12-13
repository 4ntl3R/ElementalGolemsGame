using UnityEngine;

namespace Spells.Parts
{
    [System.Serializable]
    public class FormPart: ISpellPart
    {
        public FormPart(SpellPartInfo info, float baseManaCost, float multiplierManaCost, FormEnum formName, int formValue)
        {
            _info = info;
            _baseManaCost = baseManaCost;
            _multiplierManaCost = multiplierManaCost;
            _formName = formName;
            _formValue = formValue;
        }

        [SerializeField] private SpellPartInfo _info;
        [SerializeField] private float _baseManaCost;
        [SerializeField] private float _multiplierManaCost;
        [SerializeField] private FormEnum _formName;
        [SerializeField] private int _formValue;
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
        public FormEnum formName
        {
            get => _formName;
        }
        public int formValue
        {
            get => _formValue;
        }
    }
}
