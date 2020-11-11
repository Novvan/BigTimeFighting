using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : IState
{
    private readonly GameObject _entity;
    private readonly Animator _anim;
    private readonly Rigidbody2D _rb;
    private readonly bool _isPlayer;
    private readonly Fighter _fighter;

    public Jump(GameObject entity)
    {
        _entity = entity;
        _anim = _entity.GetComponent<Animator>();
        _rb = _entity.GetComponent<Rigidbody2D>();
        if (_entity.CompareTag("Player"))
        {
            _isPlayer = true;
            _fighter = _entity.GetComponent<Fighter>();
        }
    }

    public void OnEnter()
    {
        _rb.AddForce(new Vector2(0, _fighter.JumpForce), ForceMode2D.Impulse);
    }

    public void OnExit()
    { }

    public void Tick()
    { }
}
