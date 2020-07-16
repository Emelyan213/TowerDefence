using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Navigation;
using Source.Enemy;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    private List<Enemy> _enemies = new List<Enemy>();



    private void Awake()
    {
    }



    public void AddEnemy(Enemy enemy)
    {

    }

    public void RemoveEnemy(Enemy enemy)
    {

    }


    public Enemy GetNearestEnemy(Vector3 position, float findDistance)
    {
        if (_enemies.Count == 0)
            return null;

        var nearestEnemy = _enemies[0];

        foreach (var enemy in _enemies)
        {
            var distanceToCurrentEnemy = Vector3.Distance(enemy.Position, position);
            var distanceToNearestEnemy = Vector3.Distance(nearestEnemy.Position, position);

            if (distanceToCurrentEnemy < distanceToNearestEnemy)
                nearestEnemy = enemy;
        }

        if (Vector3.Distance(nearestEnemy.Position, position) > findDistance)
            nearestEnemy = null;

        return nearestEnemy;
    }


}
