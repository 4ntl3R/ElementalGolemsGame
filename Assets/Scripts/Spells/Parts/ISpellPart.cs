using UnityEngine;

namespace Spells.Parts
{
    public interface ISpellPart
    {
        public SpellPartInfo Info { get; }
        public float BaseManaCost { get; }
        public float MultiplierManaCost { get; }
    }
}
