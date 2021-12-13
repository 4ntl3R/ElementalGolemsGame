using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Base
{
    [Serializable]
    public struct BaseActual
    {
        public float baseValue;
        [HideInInspector]
        public float actualValue;

        public BaseActual(float baseValue)
        {
            this.baseValue = baseValue;
            actualValue = baseValue;
        }
    }
    
    [Serializable]
    public class Characterizable
    {
        public event Action OnZeroValue;
        public event Action<float, float> OnValueChange;
        
        [SerializeField] protected BaseActual max;
        [SerializeField] protected BaseActual current;
        [SerializeField] protected BaseActual increase;
        [FormerlySerializedAs("IsRecovering")] public bool isRecovering;
        
        
        public Characterizable(float max, float current, float increase, bool isRecovering = false)
        {
            this.max = new BaseActual(max);
            this.current = new BaseActual(current);
            this.increase = new BaseActual(increase);
            this.isRecovering = isRecovering;
        }

        public void OnTurnTransition()
        {
            if (isRecovering)
            {
                IncreaseValue(increase.actualValue);
            }
        }

        public void IncreaseValue(float increaseValue, bool overTheMax = false)
        {
            if ((overTheMax) || (current.actualValue < max.actualValue))
            {
                current.actualValue += increaseValue;
            }
            SendValues();
        }
        
        public void DecreaseValue(float decreaseValue)
        {
            current.actualValue -= decreaseValue;
            if (current.actualValue <= 0)
            {
                OnZeroValue?.Invoke();
                current.actualValue = 0;
            }
            SendValues();
        }

        public bool IsAboveZero(float potentialDecrease = 0)
        {
            return (current.actualValue - potentialDecrease) > 0;
        }


        public void ResetMax()
        {
            max.actualValue = max.baseValue;
        }
        
        public void ResetCurrent()
        {
            current.actualValue = current.baseValue;
            SendValues();
        }
        
        public void ResetIncrease()
        {
            increase.actualValue = increase.baseValue;
        }

        public void ChangeMax(float changeValue)
        {
            var valueOverMax = Mathf.Max(current.actualValue - max.actualValue, 0);
            max.actualValue += changeValue;
            current.actualValue = Mathf.Min(current.actualValue, max.actualValue + valueOverMax);
            SendValues();
        }

        public void ChangeIncrease(float changeValue)
        {
            increase.actualValue += changeValue;
        }
        
        public void SetMax(float setValue)
        {
            var valueOverMax = Mathf.Max(current.actualValue - max.actualValue, 0);
            max.actualValue = setValue;
            current.actualValue = Mathf.Min(current.actualValue, max.actualValue + valueOverMax);
            SendValues();
        }

        public void SetIncrease(float setValue)
        {
            increase.actualValue = setValue;
        }
        
        public void SetCurrent(float setValue)
        {
            current.actualValue = setValue;
            SendValues();
        }
        
        public void SetCurrent()
        {
            SetCurrent(0);
        }

        public void SendValues()
        {
            OnValueChange?.Invoke(current.actualValue, max.actualValue);
        }

        public float GetCurrentValue()
        {
            return current.actualValue;
        }
        

        public void Init()
        {
            ResetCurrent();
            ResetIncrease();
            ResetMax();
            SendValues();
        }

    }
}
