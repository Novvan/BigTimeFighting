using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _jumpCount;
    private int _currentJumps = 0;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponentInParent<Rigidbody2D>();
        if (_jumpCount == 0) _jumpCount = 1;
    }
    void Update()
    {
        _rb.velocity = new Vector2(0, _rb.velocity.y);

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            _rb.velocity = new Vector2(_speed * Input.GetAxisRaw("Horizontal"), _rb.velocity.y);
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Environment")) _currentJumps = 0;
    }
}
