using System.Collections;
using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.EntityComponents
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 5;

        private IHealthTarget _target;

        public int HP { get; private set; }

        private void Awake()
        {
            HP = _maxHealth;
            _target = GetComponent<IHealthTarget>();
        }

        public void AdjustHealth(int amount)
        {
            if (amount < 0 && (!_target.AcceptDamage() || HP <= 0)) return;
            HP += amount;
            if (HP > _maxHealth) HP = _maxHealth;
            // TODO: Is it even worth using messaging here if I already have the reference?
            if (amount < 0) ExecuteEvents.Execute<IHealthTarget>(gameObject, null, (x, y) => x.Damaged(amount));
            if (HP <= 0) ExecuteEvents.Execute<IHealthTarget>(gameObject, null, (x,y) => x.Die());
        }
    }
}