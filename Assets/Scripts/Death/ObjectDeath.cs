using System;
using Positions;
using UnityEngine;

namespace Death
{
    public class ObjectDeath : MonoBehaviour
    {
        [SerializeField] private Positionable remains;
        private Damageable damageable;
        public event Action<Damageable> OnDeath;
        void Awake()
        {
            damageable = GetComponent<Damageable>();
            damageable.damage.OnZeroValue += OnZeroHp;
        }

        public void OnZeroHp()
        {
            var thisPosition = GetComponent<Positionable>().positionIndex;
            PositionsHandler.Instance.ForceSpawnOne(thisPosition, remains);
            OnDeath?.Invoke(damageable);
            Destroy(gameObject);
        }
    }
}
