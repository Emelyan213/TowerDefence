using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Navigation;
using UnityEngine;

namespace Source.Enemy
{
    public class KnightEnemy : Enemy, IDamage
    {
        public event Action<int> onSetMainTowerDamage;
        public event Action<Enemy, int> onDeath;

        private void Awake()
        {
            GetComponent<Mover>().onCameToEndPoint += Death;
        }

        public new void SetParameters(EnemyInfo parameters)
        {
            base.SetParameters(parameters);

            GetComponentInChildren<SpriteRenderer>().sprite = parameters.sprite;
        }

        public void GetDamage(int damagePower)
        {
            _health -= damagePower;

            if (_health < 0)
            {
                onDeath?.Invoke(this, _goldCoinCount);
                Destroy(gameObject);
            }
        }

        public void Death()
        {
            onSetMainTowerDamage?.Invoke(_damagePower);
            onDeath?.Invoke(this, 0);
            Destroy(gameObject);
        }

        public void SetDamage()
        {
            throw new NotImplementedException();
        }
    }
}

