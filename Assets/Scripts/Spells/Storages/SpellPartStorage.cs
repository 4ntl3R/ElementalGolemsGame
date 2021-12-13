using System.Collections.Generic;
using UnityEngine;

namespace Spells.Storages
{
    public abstract class SpellPartStorage<T>:ScriptableObject, ISpellPartGet<T>
    {
        [SerializeField]
        protected List<T> spellParts;

        public T[] GetParts(int amount)
        {
            var ans = new T[amount];
            for (var i = 0; i < amount; i++)
            {
                ans[i] = GetRandomPart();
            }
            return ans;
        }

        protected T GetRandomPart() => spellParts[Random.Range(0, spellParts.Count)];
    }
}
