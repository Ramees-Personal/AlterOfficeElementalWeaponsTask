using UnityEngine;
using UnityEngine.Pool;

namespace WeaponSystem.Weapon
{
    public abstract class Weapon : MonoBehaviour
    {
        protected static readonly int IsFiringAnimationID = Animator.StringToHash("IsFiring");
        protected string WeaponName => name;

        [SerializeField] protected Animator animator;

        [SerializeField] protected Transform firePoint;
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] protected AudioClip fireSound;

        private ObjectPool<Projectile> _projectilePool;

        public bool IsFiring { get; private set; }

        public virtual void StartFiring()
        {
            Debug.Log(WeaponName + " start fire..");
            IsFiring = true;
            animator.SetBool(IsFiringAnimationID, true);
        }

        public virtual void StopFiring(bool force = false)
        {
            IsFiring = false;
            animator.SetBool(IsFiringAnimationID, false);
        }

        public abstract void Fire();

        private void Start()
        {
            _projectilePool = new ObjectPool<Projectile>(createFunc: () =>
                {
                    var instance = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
                    instance.AttachPool(_projectilePool);
                    return instance;
                }, actionOnGet: projectile => { projectile.Activate(); },
                actionOnRelease: projectile => { projectile.Deactivate(); },
                actionOnDestroy: projectile => { Destroy(projectile.gameObject); });
        }

        protected Projectile SpawnProjectile()
        {
            var projectile = _projectilePool.Get();
            projectile.gameObject.SetActive(true);
            projectile.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);
            return projectile;
        }
    }
}