using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Navigation;
using UnityEngine;

namespace Source.Enemy
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

        public new void SetParameters(EnemyParameters parameters)
        {
            base.SetParameters(parameters);

            GetComponentInChildren<SpriteRenderer>().sprite = parameters.sprite;
        }

        public void GetDamage(int damagePower)
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
    }
}

