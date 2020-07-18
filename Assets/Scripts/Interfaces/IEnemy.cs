using System;
using UnityEngine;
using Source.Enemy;

namespace Assets.Scripts.Interfaces
{
    public interface IEnemy : IDamage
    {
        event Action<int> OnSetMainTowerDamage;
        event Action<IEnemy, int> OnDeath;
        Vector3 Position { get; }

        void SetParameters(EnemyParameters parameters);

        void Death();
    }
}
