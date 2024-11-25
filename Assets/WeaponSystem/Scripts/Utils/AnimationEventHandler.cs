using UnityEngine;
using WeaponSystem.Weapon;

namespace WeaponSystem.Utils
{
    public class AnimationEventHandler : MonoBehaviour
    {
        public WeaponController weaponController;

        public void OnFireEvent()
        {
            weaponController.TriggerFireAnimationEvent();
        }
    }
}