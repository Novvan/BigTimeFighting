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
    private IState _idle;
    private IState _move;
    private IState _jump;
    private IState _kick;
    private void Awake()
    {
        _fighter = this.gameObject.GetComponent<Fighter>();
        _stateMachine = new StateMachine();
        _inputManager = new InputManager(_fighter);
        _idle = new Idle(gameObject);
        _move = new Move(gameObject);
        _jump = new Jump(gameObject);
        _kick = new Kick(gameObject);

        //idle Transitions
        At(_idle, _move, _inputManager.move());
        At(_idle, _jump, _inputManager.jump());
        At(_idle, _kick, _inputManager.kick());

        //Move transitions
        At(_move, _idle, _inputManager.still());
        At(_move, _jump, _inputManager.jump());
        At(_move, _kick, _inputManager.kick());

        //Jump Transitions
        At(_jump, _idle, _grounded());

        //Custom Conditions
        Func<bool> _grounded() => () => 
        {
            return !_fighter.Jumping;
        };

        //AddTransition alias
        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

        _stateMachine.SetState(_idle);
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
    public void ResetState() 
    {
        _stateMachine.SetState(_idle);
    }
}