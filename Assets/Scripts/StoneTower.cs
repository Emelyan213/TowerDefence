using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneTower : Tower, IDamage
{
    private SceneController _sceneController;

    private Vector3 _position;
    private void Start()
    {
        _sceneController = FindObjectOfType<SceneController>();
        _position = transform.position;

        StartCoroutine(Shoot());
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
            var enemy = _sceneController.GetNearestEnemy(_position, _fireRange);

            if (enemy == null)
            {
                yield return new WaitForSeconds(_fireRate * 0.1f);
                continue;
            }

            enemy.GetComponent<IDamage>().GetDamage(_shootPower);
            yield return new WaitForSeconds(_fireRate);
        }
    }


    public void GetDamage(int damagePower)
    {

    }

    public void SetDamage()
    {
        throw new NotImplementedException();
    }
}
