using Assets.Scripts.EntityComponents;
using Assets.Scripts.Player;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class EnemyController : CustomMonoBehaviour, IHealthTarget
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private bool _facingRight = false;

        private PlayerController _player;
        private Animator _animator;
        private SpriteRenderer _renderer;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            _animator = GetComponent<Animator>();
            _renderer = GetComponent<SpriteRenderer>();
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
//                _renderer.flipX = !_renderer.flipX;
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

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}