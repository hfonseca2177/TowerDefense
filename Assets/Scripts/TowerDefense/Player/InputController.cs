using UnityEngine;
using UnityEngine.InputSystem;

namespace TowerDefense.Player
{
    /// <summary>
    /// New Input system implementation for player input controller
    /// </summary>
    public class InputController : BaseInputController
    {

        public void OnSelect()
        {
            PointerToWorldPosition();
        }


        protected override Vector3 GetPointerPosition()
        {
            return Mouse.current.position.ReadValue();
        }

    }
}