using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConditionManager : IConditionManager
{
    private readonly Fighter _fighter;

    public PlayerConditionManager(Fighter fighter) => _fighter = fighter;

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
    public Func<bool> punch() => () =>
    {
        return Input.GetKeyDown(KeyCode.H);
    };
    public Func<bool> hitted() => () =>
    {
        return _fighter.Hit;
    };
    public Func<bool> falseReturn() => () =>
    {
        return false;
    };
    public Func<bool> trueReturn() => () =>
    {
        return true;
    };
    public Func<bool> grounded() => () =>
    {
        return !_fighter.Jumping;
    };
}
