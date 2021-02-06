using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Navigation
{
    public class Mover : MonoBehaviour
    {
        public event Action onCameToEndPoint;
        [SerializeField] private float speed;
        [SerializeField] private float accuracy;

        private Transform _transform;
        private WayPoint[] _wayPoints;
        private int _currentPointIndex;

        private void Awake()
        {
            _transform = transform;
        }

        public void SetWayPoints(WayPoint[] wayPoints)
        {
            _wayPoints = wayPoints;
        }

        private void LateUpdate()
        {
            _transform.position =
                Vector2.MoveTowards(_transform.position, _wayPoints[_currentPointIndex].Position, speed * Time.deltaTime);

            if ((_transform.position - _wayPoints[_currentPointIndex].Position).magnitude <= accuracy)
                _currentPointIndex++;

            if(_currentPointIndex == _wayPoints.Length)
                onCameToEndPoint?.Invoke();
        }

        //public void StartMoveOnPoints()
        //{
        //    StartCoroutine(GoOnPoints());

        //    IEnumerator GoOnPoints()
        //    {
        //        while (_currentPointIndex < _wayPoints.Length)
        //        {
        //            _transform.position =
        //                Vector2.MoveTowards(_transform.position, _wayPoints[_currentPointIndex].Position, speed * Time.deltaTime);

        //            if ((_transform.position - _wayPoints[_currentPointIndex].Position).magnitude <= accuracy)
        //                _currentPointIndex++;

        //            yield return new WaitForEndOfFrame();
        //        }

        //        onCameToEndPoint?.Invoke();
        //    }
        //}
    }
}

