using System.Collections.Generic;
using Spells.Completion;
using Spells.Parts;
using Turns;
using UnityEngine;

namespace Effects
{

    internal class SpellValuePair
    {
        public SpellValuePair(CompleteSpell spell, int value)
        {
            Spell = spell;
            Value = value;
        }
        
        public CompleteSpell Spell;
        public int Value;
    }
    
    public class EffectsInvoker: ITurnTransitionable
    {
        private List<SpellValuePair> spellsToInvoke = new List<SpellValuePair>();

        public void OnTurnTransition()
        {
            CheckListForInvoke();
        }

        public void SubscribeOnInit()
        {
            TurnsLogic.Instance.OnTurnTransition += OnTurnTransition;
        }

        public void SendSpellToInvoker(CompleteSpell spell)
        {
            if (spell.FormName == FormEnum.Instant)
                InvokeSpell(spell);
            else
            {
                spellsToInvoke.Add(new SpellValuePair(spell, spell.FormValue));
            }
        }

        private void CheckListForInvoke()
        {
            var listToRemove = new List<SpellValuePair>();
            if (spellsToInvoke.Count > 0)
                foreach (var spellValuePair in spellsToInvoke)
                {
                    spellValuePair.Value--;
                    if (spellValuePair.Spell.FormName == FormEnum.Repeatable)
                    {
                        InvokeSpell(spellValuePair.Spell);
                    }

                    if (spellValuePair.Value <= 0)
                    {
                        if (spellValuePair.Spell.FormName == FormEnum.Delayed)
                        {
                            InvokeSpell(spellValuePair.Spell);
                        }
                        listToRemove.Add(spellValuePair);
                    }
                }
            foreach (var spellValuePair in listToRemove)
            {
                spellsToInvoke.Remove(spellValuePair);
            }
        }

        private void InvokeSpell(CompleteSpell spell)
        {
            spell.LaunchEffect();
        }
    }
}
