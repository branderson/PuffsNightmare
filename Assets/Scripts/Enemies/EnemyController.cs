using Assets.EntityComponents;
using Assets.Interfaces;
using Assets.Player;
using Assets.Utility;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Enemies
{
    public class EnemyController : CustomMonoBehaviour, IHealthTarget
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private int _damage = 1;
        [SerializeField] private int _score = 1;
        [SerializeField] private bool _facingRight = false;
        [SerializeField] private GameObject _deathObject;

        private PlayerController _player;
        private Health _playerHealth;
        private Animator _animator;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            _playerHealth = _player.GetComponent<Health>();
            _animator = GetComponent<Animator>();

            // Tell listeners an enemy has spawned
            ExecuteEvents.ExecuteHierarchy<IEnemyStatusTarget>(gameObject, null, (x, y) => x.EnemySpawned());
        }

        private void Update()
        {
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

            Vector3 moveDir = new Vector3(_player.transform.position.x - transform.position.x, 
                _player.transform.position.y - transform.position.y);
            moveDir.Normalize();
            moveDir *= _moveSpeed * Time.deltaTime;

            if ((_facingRight && moveDir.x < 0 || !_facingRight && moveDir.x > 0))
            {
                transform.localScale = new Vector3(transform.localScale.x*-1, 
                    transform.localScale.y, transform.localScale.z);
                _facingRight = !_facingRight;
            }

            // Only move when we should
            if (stateInfo.IsTag("Move"))
            {
                transform.position += moveDir;
            }
        }

        private void Attack()
        {
            _animator.SetTrigger("Attack");
        }

        public bool AcceptDamage()
        {
            return true;
        }

        public void Damaged(int amount)
        {
            
        }

        public void Die()
        {
            _animator.SetTrigger("Die");

            // Tell listeners an enemy has died
            ExecuteEvents.ExecuteHierarchy<IEnemyStatusTarget>(gameObject, null, (x, y) => x.EnemyDied(_score));
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject == _player.gameObject)
            {
                AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

                if (stateInfo.IsTag("Attack"))
                {
                    _playerHealth.AdjustHealth(-_damage);
                }
                else
                {
                    Attack();
                }
            }
        }
    }
}