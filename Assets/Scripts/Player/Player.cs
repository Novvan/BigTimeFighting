using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private StateMachine _stateMachine;
    private InputManager _inputManager = new InputManager();
    [SerializeField] private int _jumpCount;
    private int _currentJumps = 0;
    private Rigidbody2D _rb;
    private float _direction;

    public float Direction { get => _direction; }

    private void Awake()
    {
        _stateMachine = new StateMachine();
        var setup = new Setup(this.gameObject);
        var move = new Move(this.gameObject);
        // var jump = new Jump(this.gameObject);

        //Setup Transitions
        At(setup, move, _inputManager.move());
        // At(setup, jump, _inputManager.jump());
        //Move transitions
        At(move, setup, _inputManager.still());
        // At(move, jump, _inputManager.jump());

        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);


        _stateMachine.SetState(setup);
    }

    private void Start()
    {
        _rb = GetComponentInParent<Rigidbody2D>();
        if (_jumpCount == 0) _jumpCount = 1;
    }

    private void Update()
    {
        _direction = Input.GetAxisRaw("Horizontal");
        _stateMachine.Tick();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Environment")) _currentJumps = 0;
    }
}