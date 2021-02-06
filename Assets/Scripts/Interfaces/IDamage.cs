using System;

namespace Assets.Scripts.Interfaces
{
    public interface IDamage
    {
        event Action OnGetDamage;
        void GetDamage(float damagePower);
    }
}
