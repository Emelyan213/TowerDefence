using UnityEngine;

namespace Assets.Scripts.Navigation
{
    public class WayPoint : MonoBehaviour
    {
        public Vector3 Position { get; private set; }

        private void Awake()
        {
            Position = transform.position;
        }
    }
}
