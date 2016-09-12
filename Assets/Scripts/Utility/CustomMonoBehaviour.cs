using UnityEngine;

namespace Assets.Scripts.Utility
{
    public class CustomMonoBehaviour : MonoBehaviour
    {
        private Transform _transform;

        /// <summary>
        /// Caches transform the first time this is called
        /// </summary>
        public new Transform transform
        {
            get
            {
                if (_transform) return _transform;
                _transform = base.transform;
                return transform;
            }
        }
    }
}