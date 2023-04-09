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
            //when left mouse is triggered
            if (!Input.GetMouseButton(0)) return;
            TranslatePointerToWorldGroundPosition();
        }

        protected override Vector3 GetPointerPosition()
        {
            return Input.mousePosition;
        }
    }
}