using UnityEngine;

namespace WeaponSystem.Weapon
{
    public class ElectricProjectile : Projectile
    {
        [SerializeField] private Transform[] positions;

        [SerializeField] private Transform impactEffect;

        public void UpdateHitPosition(Vector3 firePoint, Vector3 hitPosition, bool hasImpact = false)
        {
            var index = 0;
            var division = 1f / (positions.Length - 1);
            foreach (var position in positions)
            {
                var targetPosition = Vector3.Lerp(firePoint, hitPosition, division * index);
                position.position = targetPosition;
                index++;
            }

            impactEffect.position = hitPosition;
            impactEffect.gameObject.SetActive(hasImpact);
        }
    }
}