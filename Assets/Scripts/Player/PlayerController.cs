using Assets.Scripts.EntityComponents;
using Assets.Scripts.Utility;
using Assets.Scripts.Utility.Pooling;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerController : CustomMonoBehaviour, IHealthTarget
    {
        [SerializeField] private PooledMonoBehaviour _bulletPrefab;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _maxFireRate = 1f;
        [SerializeField] private int _invincibleFrames = 90;

        private PlayerUIController _uiController;
        private Animator _animator;
        private ReticleController _reticle;

        private int _invincible = 0;
        private bool _facingRight = true;
        private int _shootFrames = 0;

        private void Awake()
        {
            _uiController = GetComponent<PlayerUIController>();
            _animator = GetComponent<Animator>();
            _reticle = GetComponentInChildren<ReticleController>();
        }
	
        private void Update()
        {
            UpdateCounters();
            HandleMovement();
            HandleReticle();
            HandleShooting();
        }

        private void UpdateCounters()
        {
            if (_invincible > 0) _invincible--;          
            _shootFrames ++;
        }

        private void HandleMovement()
        {
            float hor = Input.GetAxis("LeftHorizontal");
            float ver = Input.GetAxis("LeftVertical");
            float hSpeed = hor*_moveSpeed;
            float vSpeed = ver*_moveSpeed;

            if (Mathf.Abs(hor) > 0 || Mathf.Abs(ver) > 0)
            {
                transform.AdjustPosition(hSpeed*Time.deltaTime, vSpeed*Time.deltaTime);
                _animator.SetBool("Forward", (hor > 0 && _facingRight) || (hor < 0 && !_facingRight));
            }
            _animator.SetFloat("MoveSpeed", Mathf.Abs(hSpeed) + Mathf.Abs(vSpeed));
        }

        private void HandleReticle()
        {
            float hor = Input.GetAxis("RightHorizontal");
            float ver = Input.GetAxis("RightVertical");

            if (Mathf.Abs(hor) > 0 || Mathf.Abs(ver) > 0)
            {
                _reticle.SetDirection(hor, ver, _facingRight);

                if (_facingRight && hor < 0 || !_facingRight && hor > 0)
                {
                    // Flip the character
                    transform.localScale = new Vector3(transform.localScale.x*-1, 
                        transform.localScale.y, transform.localScale.z);
                    _facingRight = !_facingRight;
                }
            }
        }

        private void HandleShooting()
        {
            // Calculate what fraction of the max fire rate to shoot at
            // Fire more quickly the farther the trigger is held down
            float fireRate = Input.GetAxis("Fire");
            fireRate *= _maxFireRate;

            // Shoot fireRate times per second
            if (fireRate > 0 && _shootFrames > 60/fireRate)
            {
                _shootFrames = 0;
                Vector2 direction = _reticle.GetDirection(_facingRight);

                BulletController bullet = _bulletPrefab.GetPooledInstance<BulletController>();

                // Ignore collision between player and bullet
                Collider2D bulletCollider = bullet.GetComponent<Collider2D>();
                foreach (Collider2D playerCollider in GetComponents<Collider2D>())
                {
                    Physics2D.IgnoreCollision(playerCollider, bulletCollider);
                }
                bullet.transform.position = GetBulletSpawnPosition();
                bullet.SetDirection(direction);
            }
        }

        /// <summary>
        /// Gets the position to spawn a bullet at
        /// </summary>
        /// <returns>
        /// Position to spawn a bullet at
        /// </returns>
        private Vector2 GetBulletSpawnPosition()
        {
            return transform.position;
        }

        public bool AcceptDamage()
        {
            return _invincible <= 0;
        }

        public void Damaged(int amount)
        {
            _uiController.UpdateHP();
            _invincible = _invincibleFrames;
        }

        public void Die()
        {
        }
    }
}
