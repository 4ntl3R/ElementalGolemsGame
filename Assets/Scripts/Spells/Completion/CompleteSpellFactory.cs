using Spells.Parts;

namespace Spells.Completion
{
    public static class CompleteSpellFactory
    {
        public static CompleteSpell GetSpell(PositionsPart positionsPart, FormPart formPart, PowerPart powerPart,
            EffectPart effectPart)
        {
            ISpellPart[] partsArray = {positionsPart, formPart, powerPart, effectPart};
            return new CompleteSpell(GetCompositeInfo(partsArray), effectPart.effectName, formPart.formName,
                CountManaCost(partsArray), formPart.formValue, positionsPart.PositionIndexes, powerPart.Power);
        }

        private static SpellPartInfo GetCompositeInfo(ISpellPart[] parts)
        {
            var titleNew = "";
            var descriptionNew = "";
            var lastIndex = parts.Length - 1;
            for (int i = 0; i < lastIndex; i++)
            {
                titleNew += parts[i].Info.spellNamePiece.ToLower() + " ";
                descriptionNew += parts[i].Info.description + "\t";
            }

            titleNew += "spell of " + parts[lastIndex].Info.spellNamePiece.ToLower();
            titleNew = char.ToUpper(titleNew[0]) + titleNew.Substring(1);
            descriptionNew += parts[lastIndex].Info.description;
            return new SpellPartInfo(titleNew, descriptionNew, parts[lastIndex].Info.icon, "");
        }

        private static float CountManaCost(ISpellPart[] parts)
        {
            var multiplier = 1f;
            var baseCost = 0f;
            for (int i = 0; i < parts.Length; i++)
            {
                multiplier *= parts[i].MultiplierManaCost;
                baseCost += parts[i].BaseManaCost;
            }
            return multiplier * baseCost;
        }
        
        
    }
}
