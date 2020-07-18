using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Tower")]
public class TowerInfo : ScriptableObject
{
    public Sprite sprite;
    public int shootPower;
    public float fireRate;
    public float fireRange;
}
