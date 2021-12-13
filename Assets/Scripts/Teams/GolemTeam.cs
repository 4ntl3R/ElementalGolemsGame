using UnityEngine;

namespace Teams
{
    public class GolemTeam : MonoBehaviour
    {
        [SerializeField]
        private TeamsEnum team;

        [SerializeField] private SpriteRenderer golemRenderer;

        [SerializeField] private SpriteRenderer shadowRenderer;
        public TeamsEnum Team
        {
            get
            {
                return team;
            }
            set
            {
                team = value;
                ChangeVisuals();
            }
        }

        private void ChangeVisuals()
        {
            golemRenderer.flipX = (team == TeamsEnum.Enemy);
            shadowRenderer.flipX = (team == TeamsEnum.Enemy);
            if (team == TeamsEnum.Enemy)
                shadowRenderer.color = new Color(0, 0, 0, 0.5f);
        }
    }
}
