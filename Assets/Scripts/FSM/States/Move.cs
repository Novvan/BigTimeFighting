using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : IState
{
    const string STATE_ANIMATION = "move";
    private readonly GameObject _entity;
    private readonly Rigidbody2D _rb;
    private readonly Fighter _fighter;
    private readonly Animator _anim;
    private float _direction;

    public Move(GameObject entity)
    {

        _entity = entity;
        _anim = _entity.GetComponent<Animator>();
        _rb = _entity.GetComponent<Rigidbody2D>();
        _fighter = _entity.GetComponent<Fighter>();
    }

    public void OnEnter()
    {
        _direction = _fighter.Direction;
    }

    public void Tick()
    {

        if (_direction != 0)
        {
            _rb.velocity = new Vector2(_fighter.Speed * _direction, _rb.velocity.y);
            _anim.Play(STATE_ANIMATION);
            if (_direction < 0)
            {
                _entity.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                _entity.GetComponent<SpriteRenderer>().flipX = false;

            }

        }

        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     if (_currentJumps < _jumpCount)
        //     {
        //         _rb.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        //         _currentJumps++;
        //     }
        // }
    }

    public void OnExit()
    {
        //_rb.velocity = new Vector2(0, _rb.velocity.y);
        _anim.StopPlayback();
    }
}
