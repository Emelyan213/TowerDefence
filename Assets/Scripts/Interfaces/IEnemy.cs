using System;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IEnemy : IDamage
    {
        event Action<int> OnSetMainTowerDamage;
        event Action<int> OnDeath;
        Vector3 Position { get; }

        void SetParameters(EnemyState state);

        void Death();

        void Destroy();
    }
}
