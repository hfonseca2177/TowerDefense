using UnityEngine;
using UnityEngine.InputSystem;

namespace TowerDefense.Player
{
    /// <summary>
    /// New Input system implementation for player input controller
    /// </summary>
    public class InputController : BaseInputController
    {

        //[SerializeField, Tooltip("Player pointer input event")] private PointerInputEventAsset _onTowerAcquired;
        
        public void OnPointerAction()
        {
            TranslatePointerToWorldGroundPosition();
        }


        public override Vector3 GetPointerPosition()
        {
            return Mouse.current.position.ReadValue();
        }

    }

}