using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.EntityComponents
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 5;
        private int _health;

        private void Start()
        {
            _health = _maxHealth;
        }

        public void AdjustHealth(int amount)
        {
            _health += amount;
            if (_health > _maxHealth) _health = _maxHealth;
            if (_health <= 0) ExecuteEvents.Execute<IHealthTarget>(gameObject, null, (x,y)=>x.Die());
        }
    }

    public interface IHealthTarget : IEventSystemHandler
    {
        void Die();
    }
}