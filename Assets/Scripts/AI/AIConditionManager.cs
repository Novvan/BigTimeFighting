using System;
using UnityEngine;

public class AIConditionManager : IConditionManager
{
    private readonly Fighter _fighter;

    public AIConditionManager(Fighter fighter)
    {
        _fighter = fighter;
    }

    public Func<bool> move() => () =>
    {
        return true;
    };
    public Func<bool> still() => () =>
    {
        return true;
    };
    public Func<bool> jump() => () =>
    {
        if (!_fighter.Jumping) return true;
        else return false;
    };
    public Func<bool> kick() => () =>
    {
        return true;
    };
    public Func<bool> punch() => () =>
    {
        return true;
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
