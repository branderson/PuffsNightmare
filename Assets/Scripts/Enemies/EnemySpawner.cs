using Assets.Level;
using UnityEngine;

namespace Assets.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private float _baseRate = 10;                 // Base spawn rate per minute

        private LevelController _levelController;

        private float _spawnRate;
        private int _spawnFrames = 0;

        private void Awake()
        {
            _levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
            _spawnRate = _baseRate;
        }

        private void Update()
        {
            // Spawn _spawnRate times per minute
            if (_spawnFrames > (60*60)/_spawnRate)
            {
                _spawnFrames = 0;
                Transform enemy = Instantiate(_enemyPrefab).transform;
                enemy.parent = _levelController.transform;
                enemy.position = _levelController.GetRandomFreePosition();
            }
            _spawnFrames++;
        }

        public void SetSpawnRateFactor(float factor)
        {
            _spawnRate = _baseRate*factor;
        }
    }
}