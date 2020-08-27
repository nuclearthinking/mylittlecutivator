using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class MainMenu : MonoBehaviour
    {
        public void LoadMainScene()
        {
            SceneManager.LoadScene("Main");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}