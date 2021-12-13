using System;
using AI;
using Effects;
using Positions;
using Spells.Completion;
using Spells.Factories;
using Spells.Parts;
using Spells.Storages;
using Teams;
using Turns;
using UI;
using UnityEditor;
using UnityEngine;

namespace Spells
{
    public class SpellManager : MonoBehaviour, ITurnTransitionable
    {
        [SerializeField] 
        private int pickOptions;

        [Header("Receivers")] 
        [SerializeField]
        private SpellController playerPicker;
        [SerializeField]
        private AISpellController enemyPicker;
        [Header("Parts")]
        [SerializeField]
        private EffectPartStorage effectPartStorage;
        [SerializeField]
        private FormPartStorage formPartStorage;
        [SerializeField]
        private PowerPartFactory powerPartFactory;
        [SerializeField]
        private PositionsPartFactory positionsPartFactory;

        private EffectsInvoker _invoker;
        

        private ISpellPart[][] possibleOptions;
        private CompleteSpell completeSpell;

        private void Awake()
        {
            SubscribeOnInit();
            _invoker = new EffectsInvoker();
            _invoker.SubscribeOnInit();
            SetPossibleOptions();
        }
        
        
        public void SetPossibleOptions(int partsAmount = 4)
        {
            possibleOptions = new ISpellPart[partsAmount][];
            possibleOptions[0] = positionsPartFactory.GetParts(pickOptions);
            possibleOptions[1] = formPartStorage.GetParts(pickOptions);
            possibleOptions[2] = powerPartFactory.GetParts(pickOptions);
            possibleOptions[3] = effectPartStorage.GetParts(pickOptions);
        }
        
        public void OnTurnTransition()
        {
            SendPartsToPickers();
        }
 
        public void SubscribeOnInit()
        {
            TurnsLogic.Instance.OnTurnTransition += OnTurnTransition;
        }

        private void MakePick(int[] picks)
        {
            var positions = (PositionsPart) possibleOptions[0][picks[0]];
            var form = (FormPart) possibleOptions[1][picks[1]];
            var power = (PowerPart) possibleOptions[2][picks[2]];
            var effect = (EffectPart) possibleOptions[3][picks[3]];
            completeSpell = CompleteSpellFactory.GetSpell(positions, form, power, effect);
            SendCompleteToPickers();
        }

        private void SendPartsToPickers()
        {
            if (TurnsLogic.Instance.CurrentTeam() == TeamsEnum.Enemy)
                enemyPicker.GetParts(possibleOptions, MakePick, PassTurn);
            else
                playerPicker.GetParts(possibleOptions, MakePick, PassTurn);
        }
        
        private void SendCompleteToPickers()
        {
            var isEnoughMana = TurnsLogic.Instance.CurrentGolem().magic.IsAboveZero(completeSpell.ManaCost);
            if (TurnsLogic.Instance.CurrentTeam() == TeamsEnum.Enemy) //Нужно делать проверку в другом месте
                enemyPicker.GetCompleted(completeSpell, isEnoughMana, ConfirmPick);
            else
                playerPicker.GetCompleted(completeSpell, isEnoughMana, ConfirmPick);
        }

        private void ConfirmPick()
        {
            ActivateSpell();
        }

        public void PassTurn()
        {
            TurnsLogic.Instance.CastSpellAndMakeTurn(0);
            SetPossibleOptions();
        }

        private void ActivateSpell() // вывести в отдельный класс
        {
            _invoker.SendSpellToInvoker(completeSpell);
            TurnsLogic.Instance.CastSpellAndMakeTurn(completeSpell.ManaCost);
            SetPossibleOptions();
        }

    }
}