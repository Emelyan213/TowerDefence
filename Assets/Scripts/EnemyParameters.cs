using UnityEngine;

public struct EnemyParameters
{
    public Sprite sprite;
    public int health;
    public int damagePower;
    public int goldCoinCount;

    public EnemyParameters ImproveSomeEnemyParameters(int increaseValue)
    {
        damagePower += IncreaseParameter(increaseValue);
        health += IncreaseParameter(increaseValue);
        goldCoinCount += IncreaseParameter(increaseValue);

        return this;
    }

    private int IncreaseParameter(int increaseValue)
    {
        var random = new System.Random();

        return random.Next(0, 2) == 1 ? increaseValue : 0;
    }
}
