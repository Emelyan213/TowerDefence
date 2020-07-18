using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemiesParametersController
{
    private EnemyParameters[] _enemyEnemyParameters;

    private const int IncreaseValue = 10;

    public void SetParameters(EnemyInfo[] enemiesTypeInfo)
    {
        _enemyEnemyParameters = new EnemyParameters[enemiesTypeInfo.Length];

        for (var i = 0; i < enemiesTypeInfo.Length; i++)
            _enemyEnemyParameters[i] = enemiesTypeInfo[i].GetParameters();
    }

    public EnemyParameters GetRandomParameters()
    {
        var random = new System.Random();

        return _enemyEnemyParameters[random.Next(0, _enemyEnemyParameters.Length)];
    }

    public void ImproveParameters()
    {
        for (var i = 0; i < _enemyEnemyParameters.Length; i++)
            _enemyEnemyParameters[i] = _enemyEnemyParameters[i].ImproveSomeEnemyParameters(IncreaseValue);

    }

    public EnemyParameters[] GetParameters()
    {
        return _enemyEnemyParameters.ToArray();
    }



}
