using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : IState
{
    private readonly GameObject _entity;
    private readonly Animator _anim;
    private readonly Rigidbody2D _rb;
    private readonly Fighter _fighter;
    private float _timer;
    public Hit(GameObject entity)
    {
        _entity = entity;
        _rb = _entity.GetComponent<Rigidbody2D>();
        _anim = _entity.GetComponent<Animator>();
        _fighter = _entity.GetComponent<Fighter>();
    }
    public void OnEnter()
    {
        _anim.Play("damaged");
        _rb.velocity = Vector2.zero;
        if (_fighter.Fliped)
        {
            _rb.AddForce(new Vector2(_fighter.Speed / 4, 0), ForceMode2D.Impulse);
        }
        else
        {
            _rb.AddForce(new Vector2(-_fighter.Speed / 4, 0), ForceMode2D.Impulse);
        }
    }

    public void OnExit()
    {
        _timer = 0;
        _fighter.FigterHitbox.SetActive(false);
    }

    public void Tick()
    {
        if (_timer < _anim.GetCurrentAnimatorStateInfo(0).length)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            _entity.GetComponent<Fighter>().Hit = false;
            if (_entity.CompareTag("Player"))
            {
                _entity.GetComponent<Player>().ResetState();
            }
            else if (_entity.CompareTag("Enemy"))
            {
                _entity.GetComponent<AILogicManager>().ResetState();
            }
        }
    }
}
