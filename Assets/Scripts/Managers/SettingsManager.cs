using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class SettingsManager : Singleton<SettingsManager>
    {
        public enum ControlModes
        {
            KeyboardMouse,
            Gamepad
        }

        [SerializeField] public ControlModes ControlMode = ControlModes.KeyboardMouse;
    }
}
