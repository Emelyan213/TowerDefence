using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    protected int _shootPower;
    protected float _fireRate;
    protected float _fireRange;

    protected void SetParameters(TowerInfo parameters)
    {
        _fireRate = parameters.fireRate;
        _shootPower = parameters.shootPower;
    }
}
