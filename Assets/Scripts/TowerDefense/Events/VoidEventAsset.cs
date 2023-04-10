using UnityEngine;
using UnityEngine.Events;

namespace TowerDefense.Events
{
    /// <summary>
    /// Serializable event with no parameters
    /// </summary>
    [CreateAssetMenu(menuName = "TOWER_DEFENSE/Events/Void Event Asset")]
    public class VoidEventAsset : ScriptableObject
    {
        [SerializeField] private bool _isDebugEnabled;
        public UnityEvent OnInvoked;

        public void Invoke()
        {
            if(_isDebugEnabled) Debug.Log($" Object:{name} event invoked", this);
            OnInvoked.Invoke();
        }
    }
}