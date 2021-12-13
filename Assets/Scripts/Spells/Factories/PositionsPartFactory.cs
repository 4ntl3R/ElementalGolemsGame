using System.Collections.Generic;
using Spells.Parts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Spells.Factories
{
    [CreateAssetMenu(fileName = "PositionsPartFactory", menuName = "ScriptableObjects/Factories/PositionsPart")]
    public class PositionsPartFactory: ScriptableObject, ISpellPartGet<PositionsPart>
    {
        private const float MaxBaseManaCost = 0f;
        private const float MinBaseManaCost = 0f;
        private const float MaxMultiplierManaCost = 6f;
        private const float MinMultiplierManaCost = 1f;
        
        [SerializeField]
        private int maxPossiblePositions = 6;
        [SerializeField, TextArea]
        private string defaultDescription = "Spell will affect all golems on mentioned positions";
        [SerializeField] 
        private List<Sprite> sprites;

        public PositionsPartFactory(int maxPossiblePositions)
        {
            this.maxPossiblePositions = maxPossiblePositions;
        }

        public PositionsPart[] GetPositionsParts()
        {
            var arrayLength = (int) Mathf.Pow(2, maxPossiblePositions) - 1;
            var ans = new PositionsPart[arrayLength];
            for (var i = 1; i <= arrayLength; i++)
            {
                ans[i-1] = GetSpell(i);
            }
            return ans;
        }

        public PositionsPart GetSpell(int indexOfSpell)
        {
            var positionsAffected = PositionsFromIndex(indexOfSpell);
            var amountOfPositions = positionsAffected.Length;
            var baseMana = GetManaBase(amountOfPositions);
            var multiplierMana = GetManaMultiplier(amountOfPositions);
            
            var spellPartInfo = new SpellPartInfo();
            spellPartInfo.title = GetName(positionsAffected);
            spellPartInfo.description = defaultDescription;
            spellPartInfo.icon = GetIcon(positionsAffected.Length);
            spellPartInfo.spellNamePiece = GetSpellNamePiece(positionsAffected.Length);

            return new PositionsPart(spellPartInfo, baseMana, multiplierMana, positionsAffected);
        }
        
        public PositionsPart GetSpell()
        {
            return GetSpell(Random.Range(1, (int) Mathf.Pow(2, maxPossiblePositions) - 1));
        }

        private int[] PositionsFromIndex(int index)
        {
            var ans = new List<int>();
            for (int i = maxPossiblePositions-1; i >= 0; i--)
            {
                var currBinaryValue = (int) Mathf.Pow(2, i);
                if (index >= currBinaryValue)
                {
                    ans.Add(i);
                    index -= currBinaryValue;
                }
            }
            return ans.ToArray();
        }

        private string GetName(int[] positions)
        {
            var ans = "Positions: ";
            for (int i = (positions.Length - 1); i > 0; i--)
                ans += positions[i]+1 + ", ";
            ans += positions[0]+1;
            return ans;
        }

        private Sprite GetIcon(int index)
        {
            if (sprites.Count > index)
                return sprites[index];
            return sprites[0];
        }
        
        private string GetSpellNamePiece(int positionsLength)
        {
            if (positionsLength <= 1)
                return "Pointed";
            if (positionsLength <= maxPossiblePositions / 2)
                return "Local";
            if (positionsLength != maxPossiblePositions)
                return "Massive";
            return "Universal";
        }

        private float GetManaMultiplier(int positionsLength)
        {
            var ratio = (float)(positionsLength - 1) / (maxPossiblePositions - 1);
            return Mathf.Lerp(MinMultiplierManaCost, MaxMultiplierManaCost, ratio);
        }

        private float GetManaBase(int positionsLength)
        {
            var ratio = (float)(positionsLength - 1) / (maxPossiblePositions - 1);
            return Mathf.Lerp(MinBaseManaCost, MaxBaseManaCost, ratio);
        }

        public PositionsPart[] GetParts(int amount)
        {
            var ans = new PositionsPart[amount];
            for (int i = 0; i < amount; i++)
            {
                ans[i] = GetSpell();
            }

            return ans;
        }
    }
}
