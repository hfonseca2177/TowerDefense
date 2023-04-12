
using System;
using System.Collections;
using System.Collections.Generic;
using TowerDefense.Events;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TowerDefense.Enemies
{
    /// <summary>
    /// Enemy Wave Orchestration
    /// Spreadsheet Simulation and documentation at: https://docs.google.com/spreadsheets/d/1nr-SldvAJsId-6262lTEuEvSknqsuxypzJ87Age02sg/edit?usp=sharing 
    /// </summary>
    public class WaveSystem : MonoBehaviour
    {
        [Tooltip("Wave Settings Threshold change")]
        [SerializeField] private WaveSettings[] _settings;
        [Header("Events")]
        [SerializeField] private VoidEventAsset _onToggleWaves;
        [SerializeField] private IntEventAsset _onNewStageNotify;
        [SerializeField] private IntEventAsset _onNewWaveNotify;
 
        private WaveStateEnum _state = WaveStateEnum.Disabled;
        private Dictionary<int, WaveSettings> _waveDictionary;
        private const int WavesPerStage = 10;
        private readonly int[] _fibonacciSequence = {0, 1, 1, 2, 3, 5, 8, 13, 21, 34};
        private WaveSettings _currentWaveSettings;
        private int _currentWaveIndex;
        private int _currentStage;
        private float _elapsedTime;
        private Coroutine _spawningCoroutine;


        private void Awake()
        {
            _currentWaveIndex = 0;
            _currentStage = 1;
            LoadSettings();
            CheckIfCustomWaveSettings(_currentWaveIndex);
        }

        private void OnEnable()
        {
            _onToggleWaves.OnInvoked.AddListener(OnToggleWavesEvent);
        }

        private void OnDisable()
        {
            _onToggleWaves.OnInvoked.RemoveListener(OnToggleWavesEvent);
        }

        private void OnToggleWavesEvent()
        {
            if (_state == WaveStateEnum.Disabled)
            {
                _state = WaveStateEnum.OnCooldown;
                _elapsedTime = 0;
            }
            else
            {
                _state = WaveStateEnum.Disabled;
                if(_spawningCoroutine !=null) StopCoroutine(_spawningCoroutine);
            }
        }

        private void LoadSettings()
        {
            _waveDictionary = new Dictionary<int, WaveSettings>(_settings.Length);
            foreach (var waveSettings in _settings)
            {
                _waveDictionary.Add(waveSettings.Wave, waveSettings);
            }
        }

        private void Update()
        {
            if (_state != WaveStateEnum.OnCooldown) return;
            
            if (_elapsedTime > _currentWaveSettings.WavesInterval)
            {
                _state = WaveStateEnum.Spawning;
                SpawnWave();
            }
            else
            {
                _elapsedTime += Time.deltaTime;
            }
        }

        private void SpawnWave()
        {   
            CheckIfCustomWaveSettings(_currentWaveIndex);
            //step 1 - calc stage
            int stage = GetStage(_currentWaveIndex);
            if (stage != _currentStage)
            {
                _currentStage = stage;
                _onNewStageNotify.Invoke(_currentStage);
            }
            //step 2 - get wave intensifier factor
            int intensifier = GetWaveIntensifierFactor(_currentWaveIndex);
            //step 3 - calc dump modifier
            float dumpingModifier = CalcDumpingModifier(intensifier);
            //step 4 - calc wave budget
            float budget = CalcWaveBudget(stage, dumpingModifier);
            //step 5 - sort wave content
            _onNewWaveNotify.Invoke(_currentWaveIndex);
            _spawningCoroutine = StartCoroutine(SpawnEnemies(budget));
        }

        private void CheckIfCustomWaveSettings(int incomingWave)
        {
            if (!_waveDictionary.ContainsKey(incomingWave)) return;
            _currentWaveSettings = _waveDictionary[incomingWave];
        }

        private int GetStage(int waveIndex)
        {
            return Mathf.FloorToInt((float) waveIndex / WavesPerStage) + 1;
        }

        //returns the wave index inside the stage
        private int GetStageWaveIndex(int waveIndex)
        {
            float factionInStage = (float)waveIndex / (float) WavesPerStage;
            var diff = factionInStage - Mathf.FloorToInt(factionInStage);
            return Mathf.FloorToInt(diff * 10.0f);
        }
        
        private int GetWaveIntensifierFactor(int waveIndex)
        {
            int waveIndexInStage = GetStageWaveIndex(waveIndex);
            Debug.Log(waveIndexInStage);
            return  _fibonacciSequence[waveIndexInStage];
        }

        private float CalcDumpingModifier(int intensifier)
        {
            return intensifier * _currentWaveSettings.DumpFactor + 1;
        }

        private void FinishWave()
        {
            _elapsedTime = 0;
            _state = WaveStateEnum.OnCooldown;
            _currentWaveIndex++;
        }

        private float CalcWaveBudget(int stage, float dumpingModifier)
        {
            return _currentWaveSettings.MinimumCost * Mathf.Pow(1 + stage, dumpingModifier);
        }

        private IEnumerator SpawnEnemies(float budget)
        {
            float currentBudget = budget;
            
            while (currentBudget > 0)
            {
                int randomEnemy = 0;
                if (_currentWaveSettings.Enemies.Length > 1)
                {
                    randomEnemy = Random.Range(0, _currentWaveSettings.Enemies.Length);
                }
                EnemyDefinition enemyDefinition = _currentWaveSettings.Enemies[randomEnemy];
                currentBudget -= enemyDefinition.SpawnCost;
                enemyDefinition.SpawnRequestTrigger.Invoke();
                if (currentBudget < 0) break;
                yield return new WaitForSeconds(_currentWaveSettings.SpawnInterval);
            }
            FinishWave();
        }
        
    }
}
