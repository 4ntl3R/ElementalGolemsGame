using Base;
using Elements;
using Teams;
using Turns;
using UnityEngine;

namespace Positions
{
    public class GolemBase : Damageable, ITurnTransitionable
    {
        public Characterizable magic;
        public Characterizable initiative;
        public GolemElements elements;

        public override void OnTurnTransition() //Переделать: лучше убрать эту странную функцию, а вызывать транзишоны отдельно
        {
            base.OnTurnTransition();
            magic.OnTurnTransition();
            initiative.OnTurnTransition();
        }

        public void SubscribeOnInit()
        {
            TurnsLogic.Instance.OnTurnTransition += OnTurnTransition;
        }

        public void Test()
        {
           
        }

        protected override void Init()
        {
            base.Init();
            initiative.Init();
            magic.Init();
            elements.Init();
            SubscribeOnInit();
        }

        private void Awake()
        {
            Invoke(nameof(Init), 0.1f);
        }
    }
}
