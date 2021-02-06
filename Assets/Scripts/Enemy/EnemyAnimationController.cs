using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyAnimationController : MonoBehaviour
    {
        private void Awake()
        {
            var animator = GetComponent<Animator>();

            GetComponent<IDamage>().OnGetDamage += () => animator.SetTrigger("Damage");
        }
    }
}
