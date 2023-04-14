using UnityEngine;
using TMPro;
using TowerDefense.Events;

namespace TowerDefense.UI
{
    /// <summary>
    /// Displays the current score
    /// </summary>
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentScore;
        [Tooltip("Notifies on general score update")] 
        [SerializeField] private FloatEventAsset _onScoreUpdate;

        private void OnEnable()
        {
            _onScoreUpdate.OnInvoked.AddListener(OnScoreUpdateEvent);
        }

        private void OnDisable()
        {
            _onScoreUpdate.OnInvoked.RemoveListener(OnScoreUpdateEvent);
        }

        private void OnScoreUpdateEvent(float score)
        {
            _currentScore.text = Mathf.FloorToInt(score).ToString();
        }
    }
}