using UnityEngine;

namespace CodeBase
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void OnEnable() 
            => DontDestroyOnLoad(gameObject);
    }
}
