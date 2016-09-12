using Assets.Scripts.EntityComponents;
using Assets.Scripts.Level;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class PlayerUIController : MonoBehaviour
    {
        [SerializeField] private Text _hpText;
        [SerializeField] private Text _enemyCountText;
        private Health _health;
        private int _enemies;

        private void Awake()
        {
            _health = GetComponent<Health>();
            _enemies = 0;
        }

        private void Start()
        {
            UpdateHP();
            UpdateEnemyCount();
        }

        public void UpdateHP()
        {
            _hpText.text = _health.HP.ToString();
        }

        public void UpdateEnemyCount()
        {
            _enemyCountText.text = _enemies.ToString();
        }

        public void EnemySpawned()
        {
            _enemies++;
            UpdateEnemyCount();
        }

        public void EnemyDied()
        {
            _enemies--;
            UpdateEnemyCount();
        }
    }
}