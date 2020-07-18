using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Enemy")]
public class EnemyInfo : ScriptableObject
{
    public Sprite sprite;
    public int health;
    public int damagePower;
    public int goldCoinCount;

    public EnemyParameters GetParameters()
    {
        var state = new EnemyParameters
        {
            sprite = sprite,
            health = health,
            damagePower = damagePower,
            goldCoinCount = goldCoinCount
        };

        return state;
    }
}
