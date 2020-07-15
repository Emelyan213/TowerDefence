using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Source.Navigation
{
    public class WayPoint : MonoBehaviour
    {
        public Vector3 Position => _position;
        private Vector3 _position;

        private void Awake()
        {
            _position = transform.position;
        }
    }
}
