using Assets.EntityComponents;
using Assets.Utility.Pooling;
using Assets.Utility.Static;
using UnityEngine;

namespace Assets.Player
{
    public class BulletController : PooledMonoBehaviour
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private float _speed = 10;
        [SerializeField] private int _lifetime;

        private Vector2 _direction;

        public override void Initialize()
        {
            _damage = 1;
            _lifetime = 180;
        }

        private void Update()
        {
            transform.AdjustPosition(_direction.x*_speed*Time.deltaTime, _direction.y*_speed*Time.deltaTime);
            if (_lifetime <= 0)
            {
                ReturnToPool();
            }
            _lifetime--;
        }

        public void SetDirection(Vector2 dir)
        {
            _direction = dir;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Health health = collision.gameObject.GetComponent<Health>();
            if (health)
            {
                health.AdjustHealth(-_damage);
            }
            ReturnToPool();
        }
    }
}