using UnityEngine;

namespace TowerDefense.Towers
{
    /// <summary>
    /// Helper class to Separate core calculation cost logic from the system
    /// Spreadsheet Simulation and documentation at: https://docs.google.com/spreadsheets/d/1nr-SldvAJsId-6262lTEuEvSknqsuxypzJ87Age02sg/edit?usp=sharing
    /// </summary>
    public class TowerCostHelper
    {
        private static TowerCostHelper _instance;
        public static TowerCostHelper Instance
        {
            get { return _instance ??= new TowerCostHelper(); }
        }
        
        private TowerCostHelper()
        {
        }
        
        public float GetPurchaseCost(float baseCost, int wave, float flatModifier, float percentageModifier)
        {
            return (baseCost * Mathf.Pow((1 + percentageModifier),wave)) + (flatModifier * wave);
        }
        
        public float GetUpgradeCost(float baseCost, int wave, float flatModifier, float percentageModifier)
        {
            return (baseCost * Mathf.Pow((1 + wave), percentageModifier)) + (flatModifier * wave);
        }
    }
}