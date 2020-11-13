using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private readonly Fighter _fighter;

    public InputManager(Fighter fighter) => _fighter = fighter;

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
        if (!_fighter.Jumping) return Input.GetKeyDown(KeyCode.Space);
        else return false;
    };
    public Func<bool> kick() => () =>
    {
        return Input.GetKeyDown(KeyCode.J);
    };
}
