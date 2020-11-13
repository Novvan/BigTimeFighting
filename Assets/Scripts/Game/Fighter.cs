using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{

    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;
    [SerializeField] private bool _fliped;



    private float _direction;
    private bool _jumping = false;

    public float JumpForce => _jumpForce;
    public float Speed => _speed;
    public float Direction { get => _direction; set => _direction = value; }
    public bool Jumping { get => _jumping; set => _jumping = value; }
    public bool Fliped { get => _fliped; set => _fliped = value; }
}
