using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Positions
{
    [System.Serializable]
    public struct ObjectsSpawnSettings
    {
        public Positionable item;
        public int weightSpawn;
        public int amountSpawn;
    }

    [System.Serializable]
    public class SetupPositionsFactory
    {
        [SerializeField]
        private List<ObjectsSpawnSettings> listOfObjects;
        private bool isInited = false;
        private int summaryAmount = 1;

        public List<Positionable> GetPositionables(int inputAmount)
        {
            List<Positionable> ans = new List<Positionable>();
            if (isInited == false)
                Init();
            int amountMultiplier = (inputAmount / summaryAmount) + System.Math.Sign(inputAmount % summaryAmount);

            for (var i = 0; i < amountMultiplier; i++)
            {
                foreach (var current in listOfObjects)
                {
                    for (var j = 0; j < current.amountSpawn; j++)
                    {
                        ans.Add(current.item);
                    }
                }
            }

            return ans;
        }

        public void Init()
        {
            if (!isInited)
            {
                listOfObjects.Sort((s1, s2) => s2.weightSpawn.CompareTo(s1.weightSpawn));
                summaryAmount = listOfObjects.Sum(item => item.amountSpawn);
                isInited = true;
            }
        }
    
    }
}