using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.Services.Windows
{
    public abstract class BaseWindow : MonoBehaviour
    { 
        [SerializeField] private Button _closeButton;

        private void Awake()
        {
            OnAwake();
        }

        protected virtual void OnAwake()
        {
            _closeButton.onClick.AddListener(() => Destroy(gameObject));
        }
    }
}