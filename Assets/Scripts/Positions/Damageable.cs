using Base;
using Teams;

namespace Positions
{
    public class Damageable : Positionable
    {
        public Characterizable damage;

        public virtual void OnTurnTransition()
        {
            damage.OnTurnTransition();
        }

        protected virtual void Init()
        {
            damage.Init();
        }
    }
}
