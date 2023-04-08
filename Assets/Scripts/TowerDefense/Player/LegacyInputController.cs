using UnityEngine;

namespace TowerDefense.Player
{
    /// <summary>
    /// Old Input system implementation for player input controller
    /// </summary>
    public class LegacyInputController : BaseInputController
    {
        private void Update()
        {
            if (!Input.GetMouseButton(0)) return;
            PointerToWorldPosition();
        }

        protected override Vector3 GetPointerPosition()
        {
            return Input.mousePosition;
        }
    }
}