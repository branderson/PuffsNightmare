using System.Linq;
using Assets.Scripts.Enemies;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Player;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Level
{
    public class LevelController : MonoBehaviour, IEnemyStatusTarget
    {
        [SerializeField] private float _rateFactor = 3;    // Global spawn rate mutliplier per minute
        private LevelBorders _borders;

        private PlayerUIController _uiController;
        private EnemySpawner[] _spawners;

        private int _framesElapsed = 0;
        private bool _gameOver = false;

        private void Awake()
        {
            _spawners = GameObject.FindGameObjectsWithTag("EnemySpawner").Select(item => item.GetComponent<EnemySpawner>()).ToArray();
            _uiController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUIController>();
            _borders = GetComponentInChildren<LevelBorders>();
        }

        private void Update()
        {
            if (_gameOver) return;
            _framesElapsed++;
            foreach (EnemySpawner spawner in _spawners) spawner.SetSpawnRateFactor(_rateFactor*(1 + _framesElapsed/(60*60)));
        }

        /// <summary>
        /// Gets a random position in the level that does not collide with anything
        /// </summary>
        /// <returns>
        /// A random position in the level free from collisions
        /// </returns>
        public Vector2 GetRandomFreePosition()
        {
            int attempts = 0;

            while (attempts < 1000)
            {
                Vector2 pos = new Vector2(Random.Range(_borders.Left, _borders.Right), Random.Range(_borders.Top, _borders.Bottom));
                if (Physics2D.OverlapCircle(pos, 2f) == null) return pos;
                attempts++;
            }

            Debug.LogError("[LevelController] Attempted to find free position " + attempts + " times with no success.\n" +
                "Borders are (" + _borders.Left + ", " + _borders.Bottom + ") to (" + _borders.Right + ", " + _borders.Top + ")");
            
            return new Vector2();
        }

        public void EnemySpawned()
        {
            _uiController.EnemySpawned();
        }

        public void EnemyDied(int score)
        {
            _uiController.EnemyDied(score);
        }

        public void GameOver()
        {
            foreach (EnemySpawner spawner in _spawners) spawner.SetSpawnRateFactor(0f);
            _gameOver = true;
        }
    }
}