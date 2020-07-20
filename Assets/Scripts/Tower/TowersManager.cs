using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class TowersManager : MonoBehaviour
    {
        public void Restart()
        {
            var towers = GetComponentsInChildren<Tower>();

            foreach (var tower in towers)
                tower.SetDefault();
        }
    }
}
   
