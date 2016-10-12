using Assets.Interfaces;
using Assets.Level;
using Assets.Managers;
using Assets.Utility;
using Assets.Utility.Pooling;
using Assets.Utility.Static;
using UnityEngine;

namespace Assets.Player
{
    public class PlayerController : CustomMonoBehaviour, IHealthTarget
    {
        public enum PowerUpMode
        {
            Normal,
            Flaming,
            Gooey
        }

        public enum Weapon
        {
            Normal,
            MachineGun,
            Shotgun,
        }

        [SerializeField] private PooledMonoBehaviour _bulletPrefab;
        [SerializeField] private PooledMonoBehaviour _gooSplatPrefab;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _maxFireRate = 1f;
        [SerializeField] private int _fireModeFrames = 600;
        [SerializeField] private int _fluffModeFrames = 600;
        [SerializeField] private int _hopsPerGoo = 3;
        [SerializeField] private int _invincibleFrames = 90;
        [SerializeField] private int _flashFrames = 5;

        private PlayerUIController _uiController;
        private Animator _animator;
        private SpriteRenderer _renderer;
        private ReticleController _reticle;
        private LevelController _levelController;

        private PowerUpMode _powerUpMode = PowerUpMode.Normal;
        private Weapon _weapon = Weapon.Normal;
        private bool _facingRight = true;

        private int _ammo = 0;
        private int _powerUpModeFrames = 0;
        private int _invincible = 0;
        private int _shootFrames = 0;
        private int _gooHops = 0;
        private bool _dead = false;

        private void Awake()
        {
            _uiController = GetComponent<PlayerUIController>();
            _animator = GetComponent<Animator>();
//            _renderer = GetComponent<SpriteRenderer>();
            _reticle = GetComponentInChildren<ReticleController>();
            _levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        }
	
        private void Update()
        {
            if (_dead) return;
            UpdateCounters();
            HandleMovement();
            HandleReticle();
            HandleShooting();
        }

        private void UpdateCounters()
        {
            if (_invincible > 0)
            {
                // Invincible effect
                if (_invincible%(_flashFrames*2) == 0)
                {
//                    _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, 1f);
                }
                else if (_invincible%_flashFrames == 0)
                {
//                    _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, .5f);
                }
                _invincible--;
            }
            else
            {
//                _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, 1f);
            }
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
            // TODO: Toggle between mouse/thumbstick reticle based on which is in use rather than an actual setting
            float hor = 0f;
            float ver = 0f;
            switch (SettingsManager.Instance.ControlMode)
            {
                case SettingsManager.ControlModes.Gamepad:
                    hor = Input.GetAxis("RightHorizontal");
                    ver = Input.GetAxis("RightVertical");
                    break;
                case SettingsManager.ControlModes.KeyboardMouse:
                    hor = Input.mousePosition.x - Screen.width/2f;
                    ver = Input.mousePosition.y - Screen.height/2f;
                    break;
            }

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
                if (_weapon != Weapon.Normal)
                {
                    // Decrease ammo and revert to normal if out
                    _ammo--;
                    if (_ammo <= 0)
                    {
                        _weapon = Weapon.Normal;
                    }
                }

                // Figure out which direction to shoot
                Vector2 direction = _reticle.GetDirection(_facingRight);

                // Find a pooled bullet or make one if there are none
                BulletController bullet = _bulletPrefab.GetPooledInstance<BulletController>();

                // Ignore collision between player and bullet
                Collider2D bulletCollider = bullet.GetComponent<Collider2D>();
                foreach (Collider2D playerCollider in GetComponents<Collider2D>())
                {
                    Physics2D.IgnoreCollision(playerCollider, bulletCollider);
                }

                // Fire the bullet from the correct position in the correct direction
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

        public void GetFirePowerup()
        {
            _powerUpMode = PowerUpMode.Flaming;
            _powerUpModeFrames = _fireModeFrames;
        }

        public void HopLand()
        {
            if (_powerUpMode == PowerUpMode.Gooey)
            {
                _gooHops++;
                if (_gooHops >= _hopsPerGoo)
                {
                    GooSplat splat = _gooSplatPrefab.GetPooledInstance<GooSplat>();
                    splat.transform.position = transform.position;

                    _gooHops = 0;
                }
            }
        }

        public bool AcceptDamage()
        {
            return _invincible <= 0;
        }

        public void Damaged(int amount)
        {
            _uiController.UpdateHP();
            _invincible = _invincibleFrames;
            _animator.SetTrigger("Hurt");
        }

        /// <summary>
        /// Starts death animation
        /// </summary>
        public void Die()
        {
            _animator.SetTrigger("Die");
            _dead = true;
            _reticle.gameObject.SetActive(false);
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }

        /// <summary>
        /// Called when player finishes death animation and should be disabled
        /// </summary>
        public void GameOver()
        {
            // Inform the level that the game should end
            _levelController.GameOver();

            // Inform the player's UI that the game should end
            _uiController.GameOver();
//            _renderer.enabled = false;
        }
    }
}
