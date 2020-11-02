using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Func<bool> move() => () =>
    {
        return Input.GetAxisRaw("Horizontal") != 0;
    };
    public Func<bool> still() => () =>
    {
        return Input.GetAxisRaw("Horizontal") == 0;
    };
    public Func<bool> jump() => () =>
    {
        return Input.GetKeyDown(KeyCode.Space);
    };
}
