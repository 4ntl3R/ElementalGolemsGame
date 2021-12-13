using System;
using Spells.Completion;
using Spells.Parts;

namespace Spells
{
    public interface ISpellPicker
    {
        public void GetParts(ISpellPart[][] infos, Action<int[]> onPick, Action onPassTurn);
        public void GetCompleted(CompleteSpell completed, bool isAvailable, Action onConfirm);
    }
}
