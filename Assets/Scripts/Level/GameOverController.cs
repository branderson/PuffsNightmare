using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Level
{
    public class GameOverController : MonoBehaviour
    {
        [SerializeField] private PlayerUIController _playerUI;
        [SerializeField] private Text _scoreText;

        private void OnEnable()
        {
            _scoreText.text = "Score: " + _playerUI.Score.ToString();
        }
    }
}