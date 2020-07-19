using System;

public class Player
{
    public event Action<int> OnGoldChanged;
    public event Action<int> OnHealthChanged;
    public event Action OnPlayerDeath;

    private int _startHealth;
    private int _goldCoinsCount;
    private int _health;

    public void SetStartHealth(int health)
    {
        _startHealth = health;
    }

    public void ResetValues()
    {
        _goldCoinsCount = 0;
        _health = _startHealth;

        InvokeActions();
    }

    private void InvokeActions()
    {
        OnGoldChanged?.Invoke(_goldCoinsCount);
        OnHealthChanged?.Invoke(_health);
    }

    public void DecreaseHealth(int damage)
    {
        _health -= damage;

        if (_health <= 0)
            OnPlayerDeath?.Invoke();

        OnHealthChanged?.Invoke(_health);
    }

    public bool TryToDecreaseGoldCoins(int goldCoinsCount)
    {
        if (goldCoinsCount > _goldCoinsCount)
            return false;

        DecreaseGold(goldCoinsCount);
        return true;
    }

    private void DecreaseGold(int goldCoinsCount)
    {
        _goldCoinsCount -= goldCoinsCount;
        OnGoldChanged?.Invoke(_goldCoinsCount);
    }

    public void IncreaseGold(int goldCoinsCount)
    {
        _goldCoinsCount += goldCoinsCount;
        OnGoldChanged?.Invoke(_goldCoinsCount);
    }

}
