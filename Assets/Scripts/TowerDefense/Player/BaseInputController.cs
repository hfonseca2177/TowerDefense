using UnityEngine;

namespace TowerDefense.Player
{
    /// <summary>
    /// Base Input controller - abstract code between input controller implementations
    /// </summary>
    public abstract class BaseInputController : MonoBehaviour, IPlayerInput
    {
        [SerializeField] protected float _cameraOffset = 3f;
        [SerializeField] protected GameObject _debugPrefab;
        [SerializeField] protected bool _isDebugEnabled;

        protected void PointerToWorldPosition()
        {
            var currentCamera = GetCurrentCamera();
            if (currentCamera == null) return;
            var mousePos = GetPointerPosition();
            mousePos.z = currentCamera.nearClipPlane + _cameraOffset;
            var worldPosition = currentCamera.ScreenToWorldPoint(mousePos);
            OnPointerDown(worldPosition);
        }

        protected abstract Vector3 GetPointerPosition();

        private Camera GetCurrentCamera()
        {
            return Camera.main;
        }

        public virtual void OnPointerDown(Vector3 worldPosition)
        {
            if (!_isDebugEnabled) return;
            Debug.Log($"Mouse Down at {worldPosition}");
            Instantiate(_debugPrefab, worldPosition, Quaternion.identity);
        }
    }
}