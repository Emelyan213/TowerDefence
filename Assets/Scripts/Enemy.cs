using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamage
{
    public event Action<Enemy> onDeath;
    public Vector3 Position => _transform.position;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private int _health;
    private int _goldCoinCount;
    private int _damagePower;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public void SetParameters(EnemyInfo parameters)
    {
        spriteRenderer.sprite = parameters.sprite;
        _health = parameters.health;
        _goldCoinCount = parameters.goldCoinCount;
        _damagePower = parameters.damagePower;
    }

    public void GetDamage(int damagePower)
    {
        _health -= damagePower;

        if (_health < 0)
        {
            onDeath?.Invoke(this);
            Destroy(gameObject);
        }

        print(_health);
    }

    public void SetDamage()
    {
        throw new NotImplementedException();
    }
}
