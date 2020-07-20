using Assets.Scripts.Towers;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Scriptable objects/Tower")]
    public class TowerInfo : ScriptableObject
    {
        public Sprite sprite;
        public float shootPower;
        public float fireRate;
        public float fireRange;

        public TowerState GetState()
        {
            var state = new TowerState
            {
                sprite = sprite,
                shootPower = shootPower,
                fireRate = fireRate,
                fireRange = fireRange
            };

            return state;
        }
    }
}
