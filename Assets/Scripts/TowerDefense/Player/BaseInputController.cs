using UnityEngine;

namespace TowerDefense.Player
{
    /// <summary>
    /// Base Input controller - abstract code between input controller implementations
    /// </summary>
    public abstract class BaseInputController : MonoBehaviour, IPlayerInput
    {
        [Tooltip("Layer that represents the level ground where the tower can be placed")]
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] protected GameObject _debugPrefab;
        [SerializeField] protected bool _isDebugEnabled;
 
        protected void TranslatePointerToWorldGroundPosition()
        {
            var currentCamera = GetCurrentCamera();
            if (currentCamera == null) return;
            //try get a projection of mouse/tap position to the ground
            var mousePos = GetPointerPosition();
            Ray mouseRay = currentCamera.ScreenPointToRay(mousePos);
            if(Physics.Raycast(mouseRay, out RaycastHit hitInfo, Mathf.Infinity, _groundMask))
            {
                Vector3 pointerWorldPosition = hitInfo.point;
                if(_isDebugEnabled) Debug.DrawLine(pointerWorldPosition, transform.position);
                OnPointerDown(pointerWorldPosition);
            }
        }

        public abstract Vector3 GetPointerPosition();

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