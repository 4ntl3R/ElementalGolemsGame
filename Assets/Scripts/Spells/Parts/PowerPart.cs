namespace Spells.Parts
{
    public class PowerPart : ISpellPart
    {
        public PowerPart(SpellPartInfo info, float baseManaCost, float multiplierManaCost, float power)
        {
            Info = info;
            BaseManaCost = baseManaCost;
            MultiplierManaCost = multiplierManaCost;
            Power = power;
        }

        public SpellPartInfo Info { get; }
        public float BaseManaCost { get; }
        public float MultiplierManaCost { get; }
        public float Power { get; }
        
    }
}
