using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Level
{
    public class GameOverController : MonoBehaviour
    {
        public void TryAgain()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ReturnToMenu()
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}