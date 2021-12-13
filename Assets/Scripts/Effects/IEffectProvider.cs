using System;
using Spells.Parts;

namespace Effects
{
    public interface IEffectProvider
    {
        public Action<float, int[]> GetEffectByName(EffectEnum name);
    }
}
