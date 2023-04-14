using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense.Util
{
    /// <summary>
    /// Load game scenes
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {

        public void LoadGame()
        {
            SceneManager.LoadScene(1);
        }
        
        public void LoadSummary()
        {
            SceneManager.LoadScene(2);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}