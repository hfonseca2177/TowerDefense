using UnityEngine;

namespace TowerDefense.DevTool
{
    /// <summary>
    /// Accelerates time resolution in game
    /// </summary>
    public class TimeShifter : MonoBehaviour
    {
        [SerializeField] private float _timeShiftRate = 0.2f;
        private void Update()
        {
            if (Input.GetKey(KeyCode.KeypadPlus))
            {
                Time.timeScale += _timeShiftRate;
            }
            else if (Input.GetKey(KeyCode.KeypadMinus))
            {
                Time.timeScale -= _timeShiftRate;
            }
            else if (Input.GetKey(KeyCode.Keypad0))
            {
                Time.timeScale = 0;
            }
        }
    }
}