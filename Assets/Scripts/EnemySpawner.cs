using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Navigation;
using Source.Enemy;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public EnemyInfo[] enemyTypes;

    public float delay = 1;

    public WayPoint[] wps;

    // Start is called before the first frame update
    void Start()
    {
        var random = new System.Random();

        StartCoroutine(Spawn());


        IEnumerator Spawn()
        {
            while (true)
            {
                var enemy = Instantiate(enemyPrefab, transform).GetComponent<KnightEnemy>();
                enemy.SetParameters(enemyTypes[random.Next(0, enemyTypes.Length)]);

                var mover = enemy.GetComponent<Mover>();

                mover.SetWayPoints(wps);
                mover.StartMoveOnPoints();
                _enemies.Add(enemy);

                enemy.onDeath += (ee, coin) => { _enemies.Remove(ee); print(coin); };

                yield return new WaitForSeconds(delay);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
