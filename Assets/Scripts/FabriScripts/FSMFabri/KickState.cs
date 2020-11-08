using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickState<T> : FSMState<T>
{
    Fighter _entity;
    public KickState(Fighter Entity)
    {
        _entity = Entity;
    }
    public override void Awake()
    {
        //_entity.AnimationManager.SetAnimation("kick");
    }
    public override void Execute()
    {
        _entity.Rigidbody2D.velocity = Vector2.zero;
    }
    public override void Sleep()
    {
        
    }
}
