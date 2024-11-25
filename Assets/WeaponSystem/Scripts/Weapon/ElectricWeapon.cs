using UnityEngine;
using WeaponSystem.Utils;

namespace WeaponSystem.Weapon
{
    public class ElectricWeapon : Weapon
    {
        [SerializeField] private AudioClip beamClip;
        private ElectricProjectile _activeProjectile;
        private AudioSource _beamSource;

        public override void StopFiring(bool force = false)
        {
            base.StopFiring(force);
            _activeProjectile?.Release();
            _activeProjectile = null;
            if (_beamSource != null) SoundManager.Instance.Release(_beamSource);
            _beamSource = null;
        }

        public override void Fire()
        {
            if (!IsFiring || _activeProjectile) return;

            // SoundManager.Instance.PlaySound(fireSound);
            _activeProjectile = (ElectricProjectile)SpawnProjectile();
            _activeProjectile.transform.position = Vector3.zero;
            _activeProjectile.transform.rotation = Quaternion.identity;

            _beamSource = SoundManager.Instance.PlaySound(beamClip, loop: true);
        }

        private void Update()
        {
            if (!_activeProjectile) return;

            if (Physics.Raycast(firePoint.position, Vector3.forward, out var hit))
            {
                _activeProjectile.UpdateHitPosition(firePoint.position, hit.point, true);
            }
            else
            {
                _activeProjectile.UpdateHitPosition(firePoint.position, firePoint.position + firePoint.forward * 25f);
            }
        }
    }
}