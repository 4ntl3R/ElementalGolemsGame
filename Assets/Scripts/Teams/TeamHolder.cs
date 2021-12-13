using System.Collections.Generic;
using Death;
using Positions;
using Scenes;
using Turns;
using UnityEngine;

namespace Teams
{
    public class TeamHolder : PseudoSingletonMonoBehaviour<TeamHolder>, ITurnTransitionable
    {
        [SerializeField]
        private List<GolemBase> playerGolems;
        [SerializeField]
        private List<GolemBase> enemyGolems;

        [SerializeField] private SceneController sceneController;

        public void SetTeams()
        {
            playerGolems = new List<GolemBase>();
            enemyGolems = new List<GolemBase>();
            foreach (GolemBase golem in PositionsHandler.Instance.GetAllGolems())
            {
                if (PositionsHandler.Instance.IsOnLeftSide(golem))
                {
                    golem.GetComponent<GolemTeam>().Team = TeamsEnum.Player;
                    playerGolems.Add(golem);
                }
                else
                {
                    golem.GetComponent<GolemTeam>().Team = TeamsEnum.Enemy;
                    enemyGolems.Add(golem);
                }
                golem.GetComponent<ObjectDeath>().OnDeath += ExcludeFromLists;
            }
        }

        public override void Awake()
        {
            base.Awake();
            Invoke(nameof(SetTeams), 0.1f);
            SubscribeOnInit();
        }
        
        public void ExcludeFromLists(Damageable damageable)
        {
            GolemBase golemBase = (GolemBase) damageable;
            playerGolems.Remove(golemBase);
            enemyGolems.Remove(golemBase);
        }

        private void CheckForEnd()
        {
            if ((enemyGolems.Count == 0) || (playerGolems.Count == 0))
            {
                if ((enemyGolems.Count == 0) && (playerGolems.Count == 0))
                    Draw();
                else
                {
                    if (enemyGolems.Count == 0)
                        Win();
                    if (playerGolems.Count == 0)
                        Lose();
                }
            }
        }

        private void Lose()
        {
            sceneController.LoadLostScene();
        }

        private void Win()
        {
            sceneController.LoadWinScene();
        }

        private void Draw()
        {
            sceneController.LoadDrawScene();
        }

        public void OnTurnTransition()
        {
            CheckForEnd();
        }

        public void SubscribeOnInit()
        {
            TurnsLogic.Instance.OnTurnTransition += OnTurnTransition;
        }
    }
}
