using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Pool;
using WeaponSystem.Utils;

namespace WeaponSystem.Weapon
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private float lifeTime = 10f;
        [SerializeField] private bool keepAlive = false;
        [SerializeField] private GameObject impactEffectPrefab;
        [SerializeField] private AudioClip impactSound;

        private ObjectPool<GameObject> _impactEffectPool;

        public bool IsAlive { get; private set; }

        private float _aliveTime;
        [CanBeNull] private ObjectPool<Projectile> _pool;

        private Vector3? _direction;
        private WaitForSeconds _releaseImpactEffectTimer;

        private void Start()
        {
            if (_impactEffectPool == null)
            {
                _impactEffectPool = new ObjectPool<GameObject>(() =>
                {
                    var impactEffect = Instantiate(impactEffectPrefab);
                    return impactEffect;
                }, o => { o.SetActive(true); }, o => { o.SetActive(false); });
            }

            _releaseImpactEffectTimer = new WaitForSeconds(2f);
        }

        private void OnEnable()
        {
            _aliveTime = 0;
            IsAlive = true;
        }

        public void Update()
        {
            if (!IsAlive) return;

            HandleMovement();

            if (keepAlive) return;
            if (_aliveTime >= lifeTime)
            {
                Release();
            }
            else
            {
                _aliveTime += Time.deltaTime;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (keepAlive) return;

            Release();
            if (_impactEffectPool != null && other.contactCount > 0)
            {
                if (impactSound != null) SoundManager.Instance.PlaySound(impactSound);
                var impactEffect = _impactEffectPool.Get();
                impactEffect.transform.position = other.contacts[0].point;
                impactEffect.transform.up = other.contacts[0].normal;
            }
        }

        private IEnumerator ReleaseImpactEffect(GameObject impactEffect)
        {
            yield return _releaseImpactEffectTimer;
            _impactEffectPool?.Release(impactEffect);
        }

        public void AttachPool(ObjectPool<Projectile> pool)
        {
            _pool = pool;
        }

        public void SetDirection(Vector3 direction)
        {
            _direction = direction;
        }

        private void HandleMovement()
        {
            if (_direction.HasValue)
            {
                transform.position += _direction.Value * (speed * Time.deltaTime);
            }
        }

        public void Activate()
        {
            IsAlive = true;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            IsAlive = false;
            gameObject.SetActive(false);
        }

        public void Release()
        {
            if (_pool != null)
                _pool?.Release(this);
            else
                Destroy(gameObject);
        }
    }
}