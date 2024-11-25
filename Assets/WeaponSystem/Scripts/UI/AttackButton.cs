using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace WeaponSystem.UI
{
    public class AttackButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private UnityEvent onDown;
        [SerializeField] public UnityEvent onUp;

        public void OnPointerDown(PointerEventData eventData)
        {
            onDown.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            onUp.Invoke();
        }
    }
}