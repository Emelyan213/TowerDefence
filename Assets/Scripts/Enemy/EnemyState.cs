using UnityEngine;

public struct EnemyState
{
    public Sprite sprite;
    public float health;
    public int damagePower;
    public int goldCoinCount;

    public EnemyState ImproveSomeEnemyParameters(int increaseValue)
    {
        damagePower += GetIncreaseValue(increaseValue);
        health += GetIncreaseValue(increaseValue);
        goldCoinCount += GetIncreaseValue(increaseValue);

        return this;
    }

    private int GetIncreaseValue(int increaseValue)
    {
        var random = new System.Random();

        return random.Next(0, 2) == 1 ? increaseValue : 0;
    }
}
