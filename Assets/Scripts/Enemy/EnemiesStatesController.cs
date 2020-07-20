using System.Linq;
using Assets.Scripts.ScriptableObjects;

namespace Assets.Scripts.Enemy
{
    public class EnemiesStatesController
    {
        private EnemyState[] _enemyEnemyState;

        private const int IncreaseValue = 10;

        public void SetParameters(EnemyInfo[] enemiesTypeInfo)
        {
            _enemyEnemyState = new EnemyState[enemiesTypeInfo.Length];

            for (var i = 0; i < enemiesTypeInfo.Length; i++)
                _enemyEnemyState[i] = enemiesTypeInfo[i].GetState();
        }

        public EnemyState GetRandomParameters()
        {
            var random = new System.Random();

            return _enemyEnemyState[random.Next(0, _enemyEnemyState.Length)];
        }

        public void ImproveParameters()
        {
            for (var i = 0; i < _enemyEnemyState.Length; i++)
                _enemyEnemyState[i] = _enemyEnemyState[i].ImproveSomeEnemyParameters(IncreaseValue);

        }

        public EnemyState[] GetParameters()
        {
            return _enemyEnemyState.ToArray();
        }
    }
}
