using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public int ImprovePrice => startImprovePrice * _level;
    public int startImprovePrice = 100;
    public float ShootPower => _shootPower;
    public float FireRate => _fireRate;
    public float FireRange => _fireRange;

    protected float _shootPower;
    protected float _fireRate;
    protected float _fireRange;

    private TowerState _startState;
    private int _level = 1;
    private float _improveMultiplier = 1.2f;

    protected void SetParameters(TowerInfo parameters)
    {
        _fireRate = parameters.fireRate;
        _shootPower = parameters.shootPower;
        _fireRange = parameters.fireRange;

        _startState = parameters.GetState();
    }

    public void SetDefault()
    {
        _fireRate = _startState.fireRate;
        _shootPower = _startState.shootPower;
        _fireRange = _startState.fireRange;
        _level = 1;
    }

    public void ImproveFireRate()
    {
        _fireRate /= _improveMultiplier;
        _level++;
    }
    public void ImproveShootPower()
    {
        _shootPower *= _improveMultiplier;
        _level++;
    }
    public void ImproveFireRange()
    {
        _fireRange *= _improveMultiplier;
        _level++;
    }
}
