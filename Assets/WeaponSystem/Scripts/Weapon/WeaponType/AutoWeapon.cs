using UnityEngine;
using WeaponSystem.Utils;

namespace WeaponSystem.Weapon.WeaponType
{
    public class AutoWeapon : Weapon
    {

        public override void Fire()
        {
            if (!IsFiring) return;
            var projectile = SpawnProjectile();
            projectile.SetDirection(Vector3.forward);

            SoundManager.Instance.PlaySound(fireSound);
        }
    }
}