using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Scriptable objects/Enemy")]
    public class EnemyInfo : ScriptableObject
    {
        public Sprite sprite;
        public float health;
        public int damagePower;
        public int goldCoinCount;

        public EnemyState GetState()
        {
            var state = new EnemyState
            {
                sprite = sprite,
                health = health,
                damagePower = damagePower,
                goldCoinCount = goldCoinCount
            };

            return state;
        }
    }
}

