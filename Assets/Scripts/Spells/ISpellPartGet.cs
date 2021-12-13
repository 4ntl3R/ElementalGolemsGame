using Spells.Parts;

namespace Spells
{
    public interface ISpellPartGet<T>
    {
        public T[] GetParts(int amount);
    }
}
