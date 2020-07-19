using UnityEngine;

namespace Assets.Scripts
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
   
