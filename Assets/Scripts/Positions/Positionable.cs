using System;
using System.Collections;
using UnityEngine;

namespace Positions
{
    public class Positionable : MonoBehaviour
    {
        public int positionIndex { get; set; }
        [SerializeField] protected float updateRatio = 0.1f;
        [SerializeField] protected float movingTime = 1f;

        public event Action OnMovingStart;
        public event Action OnMovingEnd;

        public void MoveTo(Vector3 pos)
        {
            StartCoroutine(MovingProcess(pos));
        }

        IEnumerator MovingProcess(Vector3 destinationPoint)
        {
            OnMovingStart?.Invoke();
            var timer = 0f;
            var StartPosition = transform.position;
            while (timer < movingTime)
            {
                yield return new WaitForSeconds(updateRatio);
                timer += updateRatio;
                transform.position = Vector3.Lerp(StartPosition, destinationPoint, timer / movingTime);
            }

            transform.position = destinationPoint;
            OnMovingEnd?.Invoke();
        }
    }
}
