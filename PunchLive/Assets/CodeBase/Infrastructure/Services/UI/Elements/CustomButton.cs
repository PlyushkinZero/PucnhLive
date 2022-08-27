using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.Services.UI.Elements
{
    public class CustomButton : Button
    {
        public event Action OnPointerUpEvent;

        public event Action OnPointerDownEvent;

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            OnPointerDownEvent?.Invoke();
        } 
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            OnPointerUpEvent?.Invoke();
        }
    }
}