using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class StoneTower : Tower
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

    private void OnMouseDown()
    {
        if (MenuManager.towerImproveMenu.IsActiveForTower(this))
            return;

        var position = Input.mousePosition;

        MenuManager.towerImproveMenu.Show(this, position);
    }


}
