using System;
using System.Collections.Generic;
using Death;
using Positions;
using Teams;
using UnityEngine;

namespace Turns
{
    [Serializable]
    public class GolemInitiativePair
    {
        public GolemBase golem;
        public float initiative;

        public GolemInitiativePair(GolemBase golem, float initiative)
        {
            this.golem = golem;
            this.initiative = initiative;
        }

        public void ChangeInitiative(float current, float max)
        {
            initiative = current;
        }
    }
    
    public class TurnsLogic : PseudoSingletonMonoBehaviour<TurnsLogic> // Переделать полность всю систему ходов и приоритетов
    {
        public event Action OnTurnTransition;  // Переделать на обращение по интерфейсу или через ивенты, а не оба сразу
        public event Action OnSpellCast;
        [SerializeField]
        private List<GolemInitiativePair> turnPriorities;

        public override void Awake()
        {
            base.Awake();
            Invoke(nameof(SetPrioritiesList), 0.3f); //Переделать
            Invoke(nameof(MakeTurn), 1f);
        }

        public void MakeTurn()
        {
            SortList();
            OnTurnTransition?.Invoke();
        }

        public void SetPrioritiesList()
        {
            turnPriorities = new List<GolemInitiativePair>();           
            List<GolemBase> bases = PositionsHandler.Instance.GetAllGolems();
            foreach (var golem in bases)
                IncludeToList(golem);
            SortList();
        }
        
        public void ExcludeFromList(Damageable damageable)
        {
            GolemBase golemBase = (GolemBase) damageable;
            turnPriorities.RemoveAll(x => x.golem.Equals(golemBase));
        }
        
        public void IncludeToList(GolemBase golem)
        {
            GolemInitiativePair pair = new GolemInitiativePair(golem, golem.initiative.GetCurrentValue());
            golem.initiative.OnValueChange += pair.ChangeInitiative;
            golem.GetComponent<ObjectDeath>().OnDeath += ExcludeFromList;
            turnPriorities.Add(pair);
        }

        private void SortList()
        {
            turnPriorities.Sort( (s2, s1) => s1.initiative.CompareTo(s2.initiative));
        }

        public GolemBase CurrentGolem()
        {
            return turnPriorities[0].golem;
        }

        public TeamsEnum CurrentTeam()
        {
            return CurrentGolem().GetComponent<GolemTeam>().Team;
        }

        public void CastSpellAndMakeTurn(float spellCost)
        {
            OnSpellCast?.Invoke();
            CurrentGolem().magic.DecreaseValue(spellCost);
            CurrentGolem().initiative.SetCurrent();
            Invoke(nameof(MakeTurn), 3f);
        }
    }
}
