using Assets.Utility;
using UnityEngine;

namespace Assets.Level
{
    public class LevelBorders : CustomMonoBehaviour
    {
        private float _minY = Mathf.Infinity;
        private float _minX = Mathf.Infinity;
        private float _maxY = -Mathf.Infinity;
        private float _maxX = -Mathf.Infinity;

        /// <summary>
        /// The y-coordinate of the top border of the level
        /// </summary>
        public float Top
        {
            get { return _maxY; }
        }

        /// <summary>
        /// The y-coordinate of the bottom border of the level
        /// </summary>
        public float Bottom
        {
            get { return _minY; }
        }

        /// <summary>
        /// The x-coordinate of the right border of the level
        /// </summary>
        public float Right
        {
            get { return _maxX; }
        }

        /// <summary>
        /// The x-coordinate of the left border of the level
        /// </summary>
        public float Left
        {
            get { return _minX; }
        }

        private void Awake()
        {
            foreach (Transform t in GetComponentsInChildren<Transform>())
            {
                if (t.position.x > _maxX) _maxX = t.position.x;
                else if (t.position.x < _minX) _minX = t.position.x;
                if (t.position.y > _maxY) _maxY = t.position.y;
                else if (t.position.y < _minY) _minY = t.position.y;
            }
        }
    }
}