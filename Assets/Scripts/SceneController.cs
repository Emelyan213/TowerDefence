using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Source.Navigation;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    private List<Enemy> _enemies = new List<Enemy>();

    public GameObject enemyPrefab;

    public EnemyInfo[] enemyTypes;

    public float delay = 1;

    public WayPoint[] wps;



    private void Awake()
    {
        var random = new System.Random();

        StartCoroutine(Spawn());


        IEnumerator Spawn()
        {
            while (true)
            {
                var enemy = Instantiate(enemyPrefab, transform).GetComponent<Enemy>();
                enemy.SetParameters(enemyTypes[random.Next(0, enemyTypes.Length)]);

                var mover = enemy.GetComponent<Mover>();

                mover.SetWayPoints(wps);
                mover.StartMoveOnPoints();
                _enemies.Add(enemy);

                enemy.onDeath += (ee) => { _enemies.Remove(ee);};

                yield return new WaitForSeconds(delay);
            }
        }
    }







    public IDamage GetNearestEnemy(Vector3 position, float findDistance)
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
