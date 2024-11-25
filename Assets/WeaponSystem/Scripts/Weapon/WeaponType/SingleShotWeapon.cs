using UnityEngine;
using WeaponSystem.Utils;

namespace WeaponSystem.Weapon.WeaponType
{
    public class SingleShotWeapon : Weapon
    {
        [SerializeField] private AudioClip attackStartClip;
        private static readonly int FireTrigger = Animator.StringToHash("FireTrigger");

        public override void StartFiring()
        {
            animator.SetTrigger(FireTrigger);
            SoundManager.Instance.PlaySound(attackStartClip);
        }

        public override void StopFiring(bool force = false)
        {
        }

        public override void Fire()
        {
            SoundManager.Instance.PlaySound(fireSound);
            var projectile = SpawnProjectile();
            var position = firePoint.position + (Vector3.forward * 5);
            position.y = 0;
            position.x += Random.Range(-1f, 1f);
            position.z += Random.Range(-1f, 1f);
            projectile.transform.position = position;
            projectile.transform.rotation = Quaternion.identity;
        }
    }
}