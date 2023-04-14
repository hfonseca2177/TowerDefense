using UnityEngine;

namespace TowerDefense.Towers
{
    /// <summary>
    /// Helper class to Separate core calculation update logic from the system
    /// Spreadsheet Simulation and documentation at: https://docs.google.com/spreadsheets/d/1nr-SldvAJsId-6262lTEuEvSknqsuxypzJ87Age02sg/edit?usp=sharing
    /// </summary>
    public class TowerUpdateHelper
    {
        private static TowerUpdateHelper _instance;
        public static TowerUpdateHelper Instance
        {
            get { return _instance ??= new TowerUpdateHelper(); }
        }
        
        private TowerUpdateHelper()
        {
        }
        
        public float GetUpgradedValue(float baseCost, int wave, float flatModifier, float percentageModifier)
        {
            return (baseCost * Mathf.Pow((1 + percentageModifier),wave)) + (flatModifier * wave);
        }
    }
}