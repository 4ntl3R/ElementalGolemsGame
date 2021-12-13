using System;
using Effects;
using Spells.Parts;

namespace Spells.Completion
{
    public class CompleteSpell
    {
        public CompleteSpell(SpellPartInfo info, EffectEnum effectName, FormEnum formName, float manaCost, int formValue, int[] positionIndexes, float power)
        {
            Info = info;
            EffectName = effectName;
            FormName = formName;
            ManaCost = manaCost;
            FormValue = formValue;
            PositionIndexes = positionIndexes;
            Power = power;
            _effect = EffectsStorage.GetEffectByName(EffectName);
        }
        
        public SpellPartInfo Info { get; }
        public EffectEnum EffectName { get; }
        public FormEnum FormName { get; }
        public int FormValue { get;  }
        public float ManaCost { get; }
        public int[] PositionIndexes { get; }
        public float Power { get; }
        
        private Action<float, int[]> _effect;

        public void LaunchEffect()
        {
            _effect?.Invoke(Power, PositionIndexes);
        }
        
    }
}
