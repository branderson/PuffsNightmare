using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Menu
{
    public class MainMenuController : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}