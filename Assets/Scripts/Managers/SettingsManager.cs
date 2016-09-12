using UnityEngine;
using System.Collections;
using Assets.Scripts.Utility;

public class SettingsManager : Singleton<SettingsManager>
{
    public enum ControlModes
    {
        KeyboardMouse,
        Gamepad
    }

    [SerializeField] public ControlModes ControlMode = ControlModes.KeyboardMouse;
}
