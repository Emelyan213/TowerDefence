using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Enemy")]
public class EnemyInfo : ScriptableObject
{
    public Sprite sprite;
    public int health;
    public int damagePower;
    public int goldCoinCount;
}
