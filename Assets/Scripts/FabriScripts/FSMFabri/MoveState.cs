using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MoveState<T> : FSMState<T>
{
    Fighter _entity;
    public MoveState(Fighter Entity) 
    {
        _entity = Entity;
    }
    public override void Awake()
    {
        
    }
    public override void Execute()
    {
        float dir = _entity.Direction;
        if (dir > 0) _entity.AnimationManager.SetAnimation("move");
        else _entity.AnimationManager.SetAnimation("movebackwards");
        _entity.Rigidbody2D.velocity = new Vector2(dir * _entity.Speed, _entity.Rigidbody2D.velocity.y);
    }
    public override void Sleep()
    {
        
    }
}
