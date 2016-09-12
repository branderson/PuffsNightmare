using Assets.Scripts.EntityComponents;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class PlayerUIController : MonoBehaviour
    {
        [SerializeField] private Text _hpText;
        private Health _health;

        private void Awake()
        {
            _health = GetComponent<Health>();
        }

        private void Start()
        {
            UpdateHP();
        }

        public void UpdateHP()
        {
            _hpText.text = _health.HP.ToString();
        }
    }
}