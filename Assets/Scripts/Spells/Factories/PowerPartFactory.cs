using System.Collections.Generic;
using Spells.Parts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Spells.Factories
{
    [CreateAssetMenu(fileName = "PowerPartFactory", menuName = "ScriptableObjects/Factories/PowerPart")]
    public class PowerPartFactory : ScriptableObject, ISpellPartGet<PowerPart>
    {
        public PowerPartFactory(int maxPower)
        {
            this.maxPower = maxPower;
        }
        
        private const float MaxBaseManaCost = 0f;
        private const float MinBaseManaCost = 0f;
        private const float MaxMultiplierManaCost = 4f;
        private const float MinMultiplierManaCost = 1f;
        
        [SerializeField]
        private int maxPower;
        [SerializeField, TextArea]
        private string defaultDescription = "Power value defines multiplier of numeral characteristic of spell";
        [SerializeField] 
        private List<Sprite> sprites;
        
        public PowerPart GetPartByIndex(int index)
        {
            var info = new SpellPartInfo(GetName(index), defaultDescription, GetIcon(index), GetSpellNamePiece(index));
            return new PowerPart(info, GetManaBase(index), GetManaMultiplier(index), index);
        }
        
        private string GetName(int index)
        {
            return "Power: "+index;
        }

        private Sprite GetIcon(int index)
        {
            if (sprites.Count > index)
                return sprites[index];
            return sprites[0];
        }
        
        private string GetSpellNamePiece(int index)
        {
            if (index <= 1)
                return "Tiny";
            if (index <= maxPower / 2)
                return "Small";
            if (index != maxPower)
                return "Large";
            return "Huge";
        }
        
        private float GetManaMultiplier(int power)
        {
            var ratio = (float)(power - 1) / (maxPower - 1);
            return Mathf.Lerp(MinMultiplierManaCost, MaxMultiplierManaCost, ratio);
        }

        private float GetManaBase(int power)
        {
            var ratio = (float)(power - 1) / (maxPower - 1);
            return Mathf.Lerp(MinBaseManaCost, MaxBaseManaCost, ratio);
        }

        public PowerPart[] GetParts(int amount)
        {
            var ans = new PowerPart[amount];
            for (int i = 0; i < amount; i++)
            {
                ans[i] = GetPartByIndex(Random.Range(1, maxPower));
            }

            return ans;
        }
        
    }
}
