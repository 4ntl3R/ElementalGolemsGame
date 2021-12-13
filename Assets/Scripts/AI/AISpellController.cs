using System;
using System.Collections;
using Spells;
using Spells.Completion;
using Spells.Parts;
using UnityEngine;
using Random = UnityEngine.Random;


namespace AI
{
    public class AISpellController : MonoBehaviour, ISpellPicker
    {
        public float updateRatio = 0.25f;
        public int triesTillPass = 10;
        private int currentTriesTillPass;
        private bool isPickDone;
        private Action<int[]> OnPickMade;
        private Action OnConfirm;
        private Action OnPassTurn;
        private int pickLength;
        private int[] pickMax;
        public void GetParts(ISpellPart[][] infos, Action<int[]> onPick, Action onPass)
        {
            currentTriesTillPass = triesTillPass;
            OnPickMade = null;
            OnPassTurn = null;
            OnPickMade = onPick;
            OnPassTurn = onPass;
            pickLength = infos.Length;
            pickMax = new int[pickLength];
            for (int i = 0; i < pickLength; i++)
                pickMax[i] = infos[i].Length;
            StartCoroutine(PickingProcess());
        }

        public void GetCompleted(CompleteSpell completed, bool isAvailable, Action onConfirm)
        {
            isPickDone = isAvailable;
            OnConfirm = onConfirm;
        }

        IEnumerator PickingProcess()
        {
            yield return new WaitForSeconds(1f);
            while ((!isPickDone) && (currentTriesTillPass > 0))
            {
                yield return new WaitForSeconds(updateRatio);
                var randomPick = GetRandomPick(pickLength, pickMax);
                OnPickMade?.Invoke(randomPick);
                currentTriesTillPass--;
            }
            if (currentTriesTillPass > 0)
                OnConfirm?.Invoke();
            else
                OnPassTurn?.Invoke();
            isPickDone = false;
        }

        private int[] GetRandomPick(int count, int[] maxes)
        {
            var ans = new int[count];
            for (int i = 0; i < count; i++)
            {
                ans[i] = Random.Range(0, maxes[i]);
            }
            return ans;
        }
    }
}
