using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Fighter))]
public class Player : MonoBehaviour
{
    private StateMachine _stateMachine;
    private InputManager _inputManager;
    private Fighter _fighter;
    private void Awake()
    {
        _fighter = this.gameObject.GetComponent<Fighter>();
        _stateMachine = new StateMachine();
        _inputManager = new InputManager(_fighter);

        var idle = new Idle(this.gameObject);
        var move = new Move(this.gameObject);
        var jump = new Jump(this.gameObject);

        //idle Transitions
        At(idle, move, _inputManager.move());
        At(idle, jump, _inputManager.jump());
        //At(idle, punck, _inputManager.punch())
        //Move transitions
        At(move, idle, _inputManager.still());
        At(move, jump, _inputManager.jump());
        //Jump Transitions
        At(jump, idle, _grounded());

        //Custom Conditions
        Func<bool> _grounded() => () => 
        {
            return !_fighter.Jumping;
        };

        //AddTransition alias
        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

        _stateMachine.SetState(idle);
    }

    private void Start()
    {
    }

    private void Update()
    {
        _fighter.Direction = Input.GetAxisRaw("Horizontal");
        _stateMachine.Tick();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Environment")) _fighter.Jumping = false;
    }
}