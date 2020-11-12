using System.Linq;
using UnityEngine;

public class Idle : IState
{
    private readonly GameObject _entity;
    private readonly bool _isPlayer;
    private readonly Animator _anim;
    private readonly Rigidbody2D _rb;

    public Idle(GameObject entity)
    {
        _entity = entity;
        _rb = _entity.GetComponent<Rigidbody2D>();
        _anim = _entity.GetComponent<Animator>();
        if (_entity.CompareTag("Player")) _isPlayer = true;
    }

    public void OnEnter()
    {
        _anim.Play("idle");
        
    }

    public void OnExit()
    {
        _anim.StopPlayback();
    }
    public void Tick()
    {
        _rb.velocity = new Vector2(0, _rb.velocity.y);
    }

}