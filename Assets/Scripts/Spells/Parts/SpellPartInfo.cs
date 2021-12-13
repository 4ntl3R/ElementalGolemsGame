using UnityEngine;

namespace Spells.Parts
{
    [System.Serializable]
    public struct SpellPartInfo
    {
        public SpellPartInfo(string title, string description, Sprite icon, string spellNamePiece)
        {
            this.title = title;
            this.description = description;
            this.icon = icon;
            this.spellNamePiece = spellNamePiece;
        }
        
        public string title;
        [TextArea] 
        public string description;
        public Sprite icon;
        public string spellNamePiece;
    }
}
