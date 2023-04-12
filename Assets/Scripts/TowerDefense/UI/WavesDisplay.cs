using TMPro;
using TowerDefense.Events;
using UnityEngine;


namespace TowerDefense.UI
{
    /// <summary>
    /// Updates Waves UI information
    /// </summary>
    public class WavesDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _stageNumber;
        [SerializeField] private TextMeshProUGUI _stageAnnouncerNumber;
        [SerializeField] private TextMeshProUGUI _waveNumber;
        [SerializeField] private TextMeshProUGUI _waveAnnouncerNumber;
        [SerializeField] private GameObject _stageAnnouncer;
        [SerializeField] private GameObject _waveAnnouncer;
        [SerializeField] private IntEventAsset _onNewStage;
        [SerializeField] private IntEventAsset _onNewWave;

        private void OnEnable()
        {
            _onNewStage.OnInvoked.AddListener(OnNewStageEvent);
            _onNewWave.OnInvoked.AddListener(OnNewWaveEvent);
        }

        private void OnDisable()
        {
            _onNewStage.OnInvoked.RemoveListener(OnNewStageEvent);
            _onNewWave.OnInvoked.RemoveListener(OnNewWaveEvent);
        }

        private void OnNewWaveEvent(int waveIndex)
        {
            var waveStr = waveIndex.ToString();
            _waveNumber.text = waveStr;
            _waveAnnouncerNumber.text = waveStr;
            _waveAnnouncer.SetActive(true);
            Invoke(nameof(DisableWaveAnnouncer), 3f);
        }

        private void OnNewStageEvent(int stage)
        {
            var stageStr = stage.ToString();
            _stageNumber.text = stageStr;
            _stageAnnouncerNumber.text = stageStr;
            _stageAnnouncer.SetActive(true);
            Invoke(nameof(DisableStageAnnouncer), 3f);
        }

        private void DisableWaveAnnouncer()
        {
            _waveAnnouncer.SetActive(false);
        }
        
        private void DisableStageAnnouncer()
        {
            _stageAnnouncer.SetActive(false);
        }
    }
}