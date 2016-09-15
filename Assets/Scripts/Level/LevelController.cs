using System.Linq;
using Assets.Scripts.Enemies;
using Assets.Scripts.Player;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Level
{
    public class LevelController : MonoBehaviour, IEnemyStatusTarget
    {
        [SerializeField] private float _rateFactor = 3;    // Global spawn rate mutliplier per minute
        [SerializeField] private int _levelWidth = 60;
        [SerializeField] private int _levelHeight = 60;

        private PlayerUIController _uiController;
        private EnemySpawner[] _spawners;

        private int _framesElapsed = 0;

        private void Awake()
        {
            _spawners = GameObject.FindGameObjectsWithTag("EnemySpawner").Select(item => item.GetComponent<EnemySpawner>()).ToArray();
            _uiController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUIController>();
        }

        private void Update()
        {
            _framesElapsed++;
            foreach (EnemySpawner spawner in _spawners) spawner.SetSpawnRateFactor(_rateFactor*(1 + _framesElapsed/(60*60)));
        }

        public Vector2 GetRandomFreePosition()
        {
            return new Vector2(Random.value*_levelWidth - _levelWidth/2f, Random.value*_levelHeight - _levelHeight/2f);
        }

        public void EnemySpawned()
        {
            _uiController.EnemySpawned();
        }

        public void EnemyDied(int score)
        {
            _uiController.EnemyDied(score);
        }
    }

    public interface IEnemyStatusTarget : IEventSystemHandler
    {
        void EnemySpawned();

        void EnemyDied(int score);
    }
}