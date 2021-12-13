using System.Collections.Generic;
using UnityEngine;

namespace Spells.Parts
{
    public class PositionsPart : ISpellPart
    {
        public PositionsPart(SpellPartInfo info, float baseManaCost, float multiplierManaCost, int[] positionIndexes)
        {
            Info = info;
            BaseManaCost = baseManaCost;
            MultiplierManaCost = multiplierManaCost;
            PositionIndexes = positionIndexes;
        }

        public SpellPartInfo Info { get; }
        public float BaseManaCost { get; }
        public float MultiplierManaCost { get; }
        public int[] PositionIndexes { get; }
    }
}
