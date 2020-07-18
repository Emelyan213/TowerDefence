using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class StoneTower : Tower, IDamage
{
    [SerializeField] private TowerInfo towerInfo;

    private EnemiesManager _enemiesManager;

    private Vector3 _position;
    private void Start()
    {
        _enemiesManager = FindObjectOfType<EnemiesManager>();
        _position = transform.position;

        StartCoroutine(Shoot());

        SetParameters(towerInfo);
    }

    public new void SetParameters(TowerInfo parameters)
    {
        base.SetParameters(parameters);
        GetComponentInChildren<SpriteRenderer>().sprite = parameters.sprite;
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            var enemy = _enemiesManager.GetNearestEnemy(_position, _fireRange);

            if (enemy == null)
            {
                yield return new WaitForEndOfFrame();
                continue;
            }

            enemy.GetDamage(_shootPower);
            yield return new WaitForSeconds(_fireRate);
        }
    }


    public void GetDamage(int damagePower)
    {

    }

}
