using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState<T> : FSMState<T>
{
    Fighter _entity;
    public IdleState(Fighter Entity) 
    {
        _entity = Entity;
    }
    public override void Awake()
    {
        _entity.AnimationManager.SetAnimation("idle");
        _entity.Rigidbody2D.velocity = Vector2.zero;
    }
    public override void Execute()
    {
        _entity.Rigidbody2D.velocity = Vector2.zero;
    }
    public override void Sleep()
    {
       
    }
}
