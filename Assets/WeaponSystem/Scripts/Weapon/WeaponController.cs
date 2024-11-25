using UnityEngine;

namespace WeaponSystem.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Weapon[] weapons;

        private int _currentWeaponIndex;

        public Weapon CurrentWeapon => weapons[_currentWeaponIndex];

        private void Start()
        {
            SwitchWeapon(0);
        }

        private void Update()
        {
            // if (Input.GetKeyDown(KeyCode.A))
            // {
            //     SwitchWeapon((_currentWeaponIndex - 1) % weapons.Length);
            // }
            //
            // if (Input.GetKeyDown(KeyCode.D))
            // {
            //     SwitchWeapon((_currentWeaponIndex + 1) % weapons.Length);
            // }
            //
            // if (Input.GetButtonDown("Fire1"))
            // {
            //     CurrentWeapon.StartFiring();
            // }
            //
            // if (Input.GetButtonUp("Fire1"))
            // {
            //     CurrentWeapon.StopFiring();
            // }
        }

        public void OnButtonDown()
        {
            CurrentWeapon.StartFiring();
        }

        public void OnButtonUp()
        {
            CurrentWeapon.StopFiring();
        }

        public void SwitchWeapon(int index)
        {
            if (CurrentWeapon.IsFiring) return;

            if (CurrentWeapon != null)
            {
                CurrentWeapon.StopFiring();
                CurrentWeapon.gameObject.SetActive(false);
            }

            _currentWeaponIndex = index;
            CurrentWeapon.gameObject.SetActive(true);
        }

        public void TriggerFireAnimationEvent()
        {
            CurrentWeapon.Fire();
        }
    }
}