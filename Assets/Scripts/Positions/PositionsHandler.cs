using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Positions
{
    public class PositionsHandler : PseudoSingletonMonoBehaviour<PositionsHandler>
    {
        [SerializeField] private int fieldCapacity = 6;
        [SerializeField] private float distanceGap = 3;
        [SerializeField] private SetupPositionsFactory _factory;
        
        private static Dictionary<int, Positionable> positionDictionary = new Dictionary<int, Positionable>();
        private bool isPlaced = false;
        

        public override void Awake()
        {
            base.Awake();
            _factory.Init();
            SpawnAll();
        }
        
        public void SpawnAll()
        {
            if (!isPlaced)
            {
                var oneSide = _factory.GetPositionables(fieldCapacity / 2);
                for (int i = 0; i < fieldCapacity / 2; i++)
                {
                    SpawnOne(i, oneSide[i]);
                    SpawnOne(fieldCapacity - i - 1, oneSide[i]);
                }

                isPlaced = true;
            }
        }
        
        private void SpawnOne(int positionIndex, Positionable positionablePrefab)
        {
            if (!positionDictionary.ContainsKey(positionIndex))
            {
                ForceSpawnOne(positionIndex, positionablePrefab);
            }
        }
        
        public void ForceSpawnOne(int positionIndex, Positionable positionablePrefab)
        {
            GameObject newSpawned = Instantiate(positionablePrefab.gameObject, transform);
            ForceMoveToIndex(newSpawned.GetComponent<Positionable>(), positionIndex);
        }

        public Positionable GetByIndex(int index) => positionDictionary[index];

        public Positionable[] GetByIndex(int[] index)
        {
            Positionable[] ans = new Positionable[index.Length];
            for (int i = 0; i < index.Length; i++)
            {
                ans[i] = GetByIndex(index[i]);
            }
            return ans;
        }

        public bool TryMoveToIndex(Positionable positionable, int newIndex)
        {
            if (positionDictionary.ContainsKey(newIndex))
                return false;
            positionable.MoveTo(GetCoords(newIndex));
            positionable.positionIndex = newIndex;
            positionDictionary.Add(newIndex, positionable);
            return true;
        }
        
        public void ForceMoveToIndex(Positionable positionable, int newIndex)
        {
            positionable.MoveTo(GetCoords(newIndex));
            positionable.positionIndex = newIndex;
            positionDictionary[newIndex] = positionable;
        }

        public void SwapPositions(Positionable first, Positionable second)
        {
            int firstPosition = first.positionIndex;
            ForceMoveToIndex(first, second.positionIndex);
            ForceMoveToIndex(second, firstPosition);
        }
        
        public void SwapPositions(int firstIndex, int secondIndex)
        {
            SwapPositions(positionDictionary[firstIndex], positionDictionary[secondIndex]);
        }

        public void ShufflePositions(int[] indexes)
        {
            for (int i = indexes.Length - 1; i >= 0; i--)
            {
                int randomIndex = Random.Range(0, i);
                SwapPositions(indexes[i], indexes[randomIndex]);
            }
        }
        public List<GolemBase> GetAllGolems()
        {
            List<GolemBase> ans = new List<GolemBase>();
            foreach (Positionable current in positionDictionary.Values.ToArray())
            {
                if (current is GolemBase)
                    ans.Add((GolemBase)current);
            }

            return ans;
        }
        public void ShufflePositions()
        {
            int[] arr = new int[positionDictionary.Keys.Count];
            for (var i = 0; i < positionDictionary.Keys.Count; i++)
            {
                arr[i] = i;
            }
            ShufflePositions(arr);
        }

        public bool IsOnLeftSide(Positionable positionable)
        {
            if (positionable.positionIndex >= (fieldCapacity / 2))
                return false;
            return true;
        }
        

        public Vector3 GetCoords(int positionNumber) => new Vector3((fieldCapacity-1)*distanceGap/-2+(positionNumber)*distanceGap, transform.position.y , 0);
        
        
    }
}
