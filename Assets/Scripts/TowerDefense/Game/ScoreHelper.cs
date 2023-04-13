using UnityEngine;

namespace TowerDefense.Game
{
    /// <summary>
    /// Helper class to Separate core calculation score logic from the system
    /// Spreadsheet Simulation and documentation at: https://docs.google.com/spreadsheets/d/1nr-SldvAJsId-6262lTEuEvSknqsuxypzJ87Age02sg/edit?usp=sharing
    /// </summary>
    public class ScoreHelper
    {
        private static ScoreHelper _instance;
        public static ScoreHelper Instance
        {
            get { return _instance ??= new ScoreHelper(); }
        }

        private ScoreHelper()
        {
        }

        public float GetScoreEnemyDeath(float enemyScore, int wave, float flatModifier, float percentageModifier)
        {
            return (enemyScore * Mathf.Pow((1 + percentageModifier), wave)) + flatModifier;
        }
        
        public float GetNewWaveCompleted(float baseScore, int wave, float flatModifier, float percentageModifier)
        {
            return (baseScore * Mathf.Pow((1 + percentageModifier), wave)) + flatModifier;
        }
        
        public float GetScoreByTimeElapsed(float elapsedTime, int wave, float flatModifier, float percentageModifier, float rate)
        {
            float baseScore = elapsedTime / rate;
            return (baseScore * Mathf.Pow((1 + percentageModifier), wave)) + flatModifier;
        }
    }
}