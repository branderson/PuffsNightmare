using Assets.Utility;
using UnityEngine;

namespace Assets.Managers
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
