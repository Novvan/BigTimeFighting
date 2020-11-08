using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState<T> : FSMState<T>
{
    Fighter _entity;
    public JumpState(Fighter Entity)
    {
        _entity = Entity;
    }
    public override void Awake()
    {
        _entity.Onfloor = false;
        if (_entity.Direction == 0)
        {
            //_entity.AnimationManager.SetAnimation("jump");
            _entity.Rigidbody2D.AddForce(new Vector2(0, _entity.JumpForce));
        }
        else
        {
           // if (_entity.Direction < 0) _entity.AnimationManager.SetAnimation("JumpBackward");
           // if (_entity.Direction > 0) _entity.AnimationManager.SetAnimation("JumpFordward");
            _entity.Rigidbody2D.AddForce(new Vector2(_entity.Speed * _entity.Direction, _entity.JumpForce));
        }


    }
    public override void Execute()
    {
        
    }
    public override void Sleep()
    {
        
    }
}
