using Assets.Scripts.EntityComponents;
using Assets.Scripts.Level;
using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    /// <summary>
    /// Middleman class between the PlayerController and the UIController
    /// </summary>
    public class PlayerUIController : MonoBehaviour
    {
        [SerializeField] private UIController _uiController;
        [SerializeField] private GameOverController _gameOverController;
        private Health _health;
        public int Score { get; private set; }
        private int _enemies;

        private void Awake()
        {
            _health = GetComponent<Health>();
            Score = 0;
            _enemies = 0;
        }

        private void Start()
        {
            _uiController.ShowHUD();
            UpdateHP();
            UpdateScore();
            UpdateEnemyCount();
        }

        public void UpdateHP()
        {
            _uiController.HP = _health.HP;
        }

        public void UpdateScore()
        {
            _uiController.Score = Score;
        }

        public void UpdateEnemyCount()
        {
            _uiController.EnemyCount = _enemies;
        }

        public void EnemySpawned()
        {
            _enemies++;
            UpdateEnemyCount();
        }

        public void EnemyDied(int score)
        {
            _enemies--;
            Score += score;
            UpdateScore();
            UpdateEnemyCount();
        }

        public void GameOver()
        {
            // Inform the UI that the game should end
            _uiController.GameOver();
        }
    }
}