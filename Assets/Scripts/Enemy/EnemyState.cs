using UnityEngine;

public struct EnemyState
{
    public Sprite sprite;
    public float health;
    public int damagePower;
    public int goldCoinCount;

    public EnemyState ImproveSomeEnemyParameters(int increaseValue)
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
