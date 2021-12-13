using System;
using Spells;
using Spells.Completion;
using Spells.Parts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class SpellController : MonoBehaviour, ISpellPicker
    {
        [SerializeField] private PartController partControllerPrefab;
        [SerializeField] private HorizontalLayoutGroup blockPrefab;
        [SerializeField] private CompletedController completedController;
        [SerializeField] private Button castButton;
        

        private PartController[][] partControllers;
        private int[] pickedParts;
        
        private Action<int[]> OnPickCompleted;
        private Action OnPickConfirmed;
        private Action OnPassTurn;

        private bool isMinimized = false; //вынести в отдельный скрипт
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            
        }

        private Vector2Int GetIndex(PartController partController)
        {
            for (int i = 0; i < partControllers.Length; i++)
                for (int j = 0; j < partControllers[i].Length; j++)
                    if (partController.Equals(partControllers[i][j]))
                        return new Vector2Int(i, j);
            return new Vector2Int(-1, -1);
        }

        private void CheckPick()
        {
            var isPicked = true;
            for (int i = 0; i < pickedParts.Length; i++)
            {
                if (pickedParts[i] == -1)
                    isPicked = false;
            }
            if (isPicked)
                OnPickCompleted?.Invoke(pickedParts);
        }
        
        public void GetParts(ISpellPart[][] infos, Action<int[]> onPick, Action onPass)
        {
            gameObject.SetActive(true);
            castButton.interactable = false;
            ResetHeader();
            partControllers = new PartController[infos.Length][];
            for (int i = 0; i < infos.Length; i++)
            {
                partControllers[i] = new PartController[infos[i].Length];
                var currentBlock = Instantiate(blockPrefab.gameObject, transform);
                currentBlock.transform.SetSiblingIndex(1);
                for (int j = 0; j < infos[i].Length; j++)
                {
                    var currentPartGameObject = Instantiate(partControllerPrefab.gameObject, currentBlock.transform);
                    var currentPart = currentPartGameObject.GetComponent<PartController>();
                    currentPart.SetResources(infos[i][j].Info);
                    partControllers[i][j] = currentPart;
                    currentPart.OnActivateButton += PickedPart;
                }
            }

            pickedParts = new int[infos.Length];

            for (int i = 0; i < pickedParts.Length; i++)
            {
                pickedParts[i] = -1;
            }

            OnPickCompleted += onPick;
            OnPassTurn += onPass;
        }

        public void GetCompleted(CompleteSpell completed, bool isAvailable, Action onConfirm)
        {
            castButton.interactable = isAvailable;
            UpdateHeader(completed);
            OnPickConfirmed += onConfirm;
        }

        private void PickedPart(PartController partController)
        {
            var indexes = GetIndex(partController);
            pickedParts[indexes.x] = indexes.y;
            for (int i = 0; i < partControllers[indexes.x].Length; i++)
            {
                if (i != indexes.y)
                    partControllers[indexes.x][i].SetBlockDisabled();
            }
            CheckPick();
        }

        public void InvokePickConfirmed() => OnPickConfirmed?.Invoke();

        public void ClearAllData()
        {
            OnPickCompleted = null;
            OnPickConfirmed = null;
            OnPassTurn = null;
            ResetHeader();
            for (int i = 1; i < transform.childCount-1; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            gameObject.SetActive(false);
        }

        private void UpdateHeader(CompleteSpell completed)
        {
            completedController.SetResources(completed.Info.title, completed.ManaCost);
        }
        
        private void ResetHeader()
        {
            completedController.SetResources("Pick spell parts", 0);
        }

        public void ToggleShowHide()
        {
            if (isMinimized)
                MaximizeMenu();
            else
                MinimizeMenu();
        }

        private void MinimizeMenu()
        {
            for (int i = 1; i < transform.childCount-1; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            _rectTransform.offsetMin = new Vector2(0, 350); //вынести в отдельную переменную
            isMinimized = true;
        }
        
        private void MaximizeMenu()
        {
            for (int i = 1; i < transform.childCount-1; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            
            _rectTransform.offsetMin = new Vector2(0, 0);
            isMinimized = false;
        }

        public void PassTurn()
        {
            OnPassTurn?.Invoke();
        }
    }
}
