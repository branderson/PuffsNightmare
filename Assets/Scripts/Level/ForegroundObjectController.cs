using Assets.Utility;
using Assets.Utility.Static;
using UnityEngine;

namespace Assets.Level
{
    public class ForegroundObjectController : CustomMonoBehaviour
    {
        [SerializeField] private float _transitionSpeed = 3f;
        private SpriteRenderer[] _renderers;

        private Color _opaque;
        private Color _translucent;
        private Color _goalColor;

        private void Awake()
        {
            _renderers = GetComponentsInChildren<SpriteRenderer>();
            _opaque = Color.white;
            _translucent = new Color(_opaque.r, _opaque.g, _opaque.b, .75f);
            _goalColor = _opaque;

            // Make objects lower on y-axis very slightly higher on z-axis (for rendering)
            transform.AdjustPosition(0, 0, transform.position.y * .01f);
        }

        private void Update()
        {
            foreach (SpriteRenderer r in _renderers)
            {
                r.color = Color.Lerp(r.color, _goalColor, _transitionSpeed * Time.deltaTime);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag != "Player") return;
            _goalColor = _translucent;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag != "Player") return;
            _goalColor = _opaque;
        }
    }
}