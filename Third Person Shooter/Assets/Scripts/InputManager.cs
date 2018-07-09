using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    public static float Vertical { get { return Input.GetAxis("Vertical"); } }
    public static float Horizontal { get { return Input.GetAxis("Horizontal"); } }
    public static float HorizontalMouse { get { return Input.GetAxis("Mouse X"); } }
    public static float VerticalMouse { get { return Input.GetAxis("Mouse Y"); } }
    public static bool Aim { get { return Input.GetKey(KeyCode.JoystickButton4); } }
    public static bool Shoot { get { return Input.GetKey(KeyCode.JoystickButton0); } }
    public static bool AimPressed { get { return Input.GetKeyDown(KeyCode.JoystickButton4); } }
    public static bool ShootPressed { get { return Input.GetKeyDown(KeyCode.JoystickButton0); } }
    public static bool ChangeWeaponPressed { get { return Input.GetKeyDown(KeyCode.JoystickButton1); } }
    public static bool TrowWeaponPressed { get { return Input.GetKeyDown(KeyCode.JoystickButton2); } }


}
