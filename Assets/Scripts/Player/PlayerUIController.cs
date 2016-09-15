using Assets.Scripts.EntityComponents;
using Assets.Scripts.Level;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class PlayerUIController : MonoBehaviour
    {
        [SerializeField] private Text _hpText;
        [SerializeField] private Text _scoreText;
        [SerializeField] private Text _enemyCountText;
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
            UpdateHP();
            UpdateScore();
            UpdateEnemyCount();
        }

        public void UpdateHP()
        {
            _hpText.text = "Health: " + _health.HP.ToString();
        }

        public void UpdateScore()
        {
            _scoreText.text = "Score: " + Score.ToString();
        }

        public void UpdateEnemyCount()
        {
            _enemyCountText.text = "Enemies: " + _enemies.ToString();
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
            _gameOverController.gameObject.SetActive(true);
            // TODO: Get rid of player
        }
    }
}