using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchState<T> : FSMState<T>
{
    Fighter _entity;
    public PunchState(Fighter Entity)
    {
        _entity = Entity;
    }
    public override void Awake()
    {

        //_entity.AnimationManager.SetAnimation("punch");
    }
    public override void Execute()
    {
        _entity.Rigidbody2D.velocity = Vector2.zero;
    }
    public override void Sleep()
    {
        
    }
}
