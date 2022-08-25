using UnityEngine;

namespace CodeBase.Components
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void OnEnable() 
            => DontDestroyOnLoad(gameObject);
    }
}
