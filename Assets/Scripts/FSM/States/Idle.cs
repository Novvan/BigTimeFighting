using System.Linq;
using UnityEngine;

public class Idle : IState
{
    private readonly GameObject _entity;
    private readonly bool _isPlayer;
    private readonly Animator _anim;

    public Idle(GameObject entity)
    {
        _entity = entity;
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
    }

}