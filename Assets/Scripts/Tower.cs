using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, IDamage
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public int _shootPower;
    public float _fireRate;
    public float _fireRange;

    private SceneController _sceneController;

    private Vector3 _position;

    private void Awake()
    {
        _sceneController = FindObjectOfType<SceneController>();
        _position = transform.position;

        StartCoroutine(Shoot());
    }

    public void SetParameters(TowerInfo parameters)
    {
        spriteRenderer.sprite = parameters.sprite;
        _fireRate = parameters.fireRate;
        _shootPower = parameters.shootPower;
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            var enemy = _sceneController.GetNearestEnemy(_position, _fireRange);

            if (enemy == null)
            {
                yield return new WaitForSeconds(_fireRate * 0.1f);
                continue;
            }

            enemy.GetDamage(_shootPower);
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
