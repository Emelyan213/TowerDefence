using System;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Navigation;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class KnightEnemy : Enemy, IEnemy
    {
        public event Action<int> OnSetMainTowerDamage;
        public event Action<IEnemy, int> OnDeath;
        public Vector3 Position => _transform ? _transform.position : transform.position;

        private void Awake()
        {
            GetComponent<Mover>().onCameToEndPoint += Death;
        }

        public new void SetParameters(EnemyState state)
        {
            base.SetParameters(state);

            GetComponentInChildren<SpriteRenderer>().sprite = state.sprite;
        }

        public void GetDamage(float damagePower)
        {
            _health -= damagePower;

            if (_health < 0)
            {
                OnDeath?.Invoke(this, _goldCoinCount);
                Destroy(gameObject);
            }
        }

        public void Death()
        {
            OnSetMainTowerDamage?.Invoke(_damagePower);
            OnDeath?.Invoke(this, 0);
            Destroy(gameObject);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}

