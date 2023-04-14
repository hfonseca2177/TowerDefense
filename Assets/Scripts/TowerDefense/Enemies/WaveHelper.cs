using UnityEngine;

namespace TowerDefense.Enemies
{
    /// <summary>
    /// Helper class to Separate core wave logic from the system
    /// Ps: Makes Good Target for unit tests 
    /// </summary>
    public class WaveHelper
    {
        private static WaveHelper _instance;
        public static WaveHelper Instance
        {
            get { return _instance ??= new WaveHelper(); }
        }

        private WaveHelper()
        {
        }

        private const int WavesPerStage = 10;
        private readonly int[] _fibonacciSequence = {0, 1, 1, 2, 3, 5, 8, 13, 21, 34};
        
        public int GetStage(int waveIndex)
        {
            return Mathf.FloorToInt((float) waveIndex / WavesPerStage) + 1;
        }

        //returns the wave index inside the stage
        public int GetStageWaveIndex(int waveIndex)
        {
            float factionInStage = (float)waveIndex / (float) WavesPerStage;
            var diff = factionInStage - Mathf.FloorToInt(factionInStage);
            return Mathf.FloorToInt(diff * 10.0f);
        }
        
        public int GetWaveIntensifierFactor(int waveIndex)
        {
            int waveIndexInStage = GetStageWaveIndex(waveIndex);
            return  _fibonacciSequence[waveIndexInStage];
        }

        public float CalcDumpingModifier(int intensifier, float dumpFactor)
        {
            return intensifier * dumpFactor + 1;
        }
        
        public float CalcWaveBudget(int stage, float dumpingModifier, float minimumCost)
        {
            return minimumCost * Mathf.Pow(1 + dumpingModifier, stage);
        }
    }
}