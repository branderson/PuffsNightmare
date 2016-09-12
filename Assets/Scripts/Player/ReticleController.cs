using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class ReticleController : CustomMonoBehaviour
    {
        [SerializeField] private float _distanceFromPlayer = 1f;

        private void Start()
        {
            transform.localPosition = Vector3.up*_distanceFromPlayer;
        }

        /// <summary>
        /// Sets the direction of the reticle in relation to the player, keeping it
        /// _distanceFromPlayer units away from the player
        /// </summary>
        /// <param name="x">
        /// X component of direction vector
        /// </param>
        /// <param name="y">
        /// Y component of direction vector
        /// </param>
        public void SetDirection(float x, float y, bool facingRight)
        {
            Vector2 position = new Vector2(x, y);
            position.Normalize();
            position *= _distanceFromPlayer;
            if (!facingRight)
            {
                position.x = -position.x;
            }
            transform.localPosition = position;
        }

        /// <summary>
        /// Get the direction of the reticle in relation to the player as a vector, normalized
        /// to 1
        /// </summary>
        /// <returns>
        /// Unit vector direction of reticle in relation to the player
        /// </returns>
        public Vector2 GetDirection(bool facingRight)
        {
            if (facingRight)
            {
                return transform.localPosition / _distanceFromPlayer;
            }
            Vector2 position = new Vector2(-transform.localPosition.x, transform.localPosition.y);
            return position / _distanceFromPlayer;
        }
    }
}