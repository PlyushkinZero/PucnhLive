using System;
using UnityEngine.EventSystems;

namespace CodeBase.Infrastructure.Services.Windows
{
    public class PauseWindow : BaseWindow, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action OnClick;

        protected override void OnAwake()
        {
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Destroy(gameObject);
            OnClick?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
        }

        public void OnPointerExit(PointerEventData eventData)
        {
        }
    }
}