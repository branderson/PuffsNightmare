using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private PlayerUIController _playerUI;
        [SerializeField] private Transform _hudPanel;
        [SerializeField] private Transform _gameOverPanel;
        [SerializeField] private Text _hpText;
        [SerializeField] private Text _hudScoreText;
        [SerializeField] private Text _gameOverScoreText;
        [SerializeField] private Text _enemyCountText;

        private enum UIState
        {
            HUD,
            GameOverScreen,
        }

        public int HP
        {
            set
            {
                _hpText.text = "Health: " + value;
            }
        }

        public int Score
        {
            set
            {
                _hudScoreText.text = "Score: " + value;
                _gameOverScoreText.text = "Score: " + value;
            }
        }

        public int EnemyCount
        {
            set
            {
                _enemyCountText.text = "Enemies: " + value;
            }
        }

        private void SetState(UIState state)
        {
            // Disable all UI elements
            _hudPanel.gameObject.SetActive(false);
            _gameOverPanel.gameObject.SetActive(false);

            switch (state)
            {
                case UIState.HUD:
                    _hudPanel.gameObject.SetActive(true);
                    break;
                case UIState.GameOverScreen:
                    _gameOverPanel.gameObject.SetActive(true);
                    break;
            }
        }

        public void ShowHUD()
        {
            SetState(UIState.HUD);
        }

        public void GameOver()
        {
            SetState(UIState.GameOverScreen);
        }
    }
}