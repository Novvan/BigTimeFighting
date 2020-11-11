using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{

    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;




    private float _direction;
    private bool _jumping = false;
    public float JumpForce { get => _jumpForce; }
    public float Direction { get => _direction; set => _direction = value; }
    public bool Jumping { get => _jumping; set => _jumping = value; }
    public float Speed => _speed;
}
