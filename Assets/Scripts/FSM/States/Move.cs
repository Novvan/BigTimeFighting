using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : IState
{
    private GameObject _entity;
    private Rigidbody2D _rb;
    private float _direction;
    private float _speed = 7.5f;
    private int _jumpCount = 1;
    private int _currentJumps = 0;
    private float _jumpForce = 10;

    public Move(GameObject entity)
    {

        _entity = entity;
        _rb = _entity.GetComponent<Rigidbody2D>();
    }

    public void OnEnter()
    {
        _direction = _entity.CompareTag("Player") ? _entity.GetComponent<Player>().Direction : _entity.GetComponent<AILogicManager>().Direction;
    }

    public void Tick()
    {

        if (_direction != 0)
        {
            _rb.velocity = new Vector2(_speed * _direction, _rb.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_currentJumps < _jumpCount)
            {
                _rb.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
                _currentJumps++;
            }
        }
    }

    public void OnExit()
    {
        _rb.velocity = new Vector2(0, _rb.velocity.y);
    }
}
