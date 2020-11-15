using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Fighter))]
public class Player : MonoBehaviour
{
    private StateMachine _stateMachine;
    private PlayerConditionManager _conditions;
    private Fighter _fighter;
    private IState _idle;
    private IState _move;
    private IState _jump;
    private IState _hit;
    private IState _kick;
    private IState _punch;

    private void Awake()
    {
        _fighter = this.gameObject.GetComponent<Fighter>();
        _stateMachine = new StateMachine();
        _conditions = new PlayerConditionManager(_fighter);
        _idle = new Idle(gameObject);
        _move = new Move(gameObject);
        _jump = new Jump(gameObject);
        _hit = new Hit(gameObject);
        _kick = new Kick(gameObject);
        _punch = new Punch(gameObject);

        //idle Transitions
        At(_idle, _move, _conditions.move());
        At(_idle, _jump, _conditions.jump());
        At(_idle, _hit, _conditions.hitted());
        At(_idle, _kick, _conditions.kick());
        At(_idle, _punch, _conditions.punch());

        //Move transitions
        At(_move, _idle, _conditions.still());
        At(_move, _jump, _conditions.jump());
        At(_move, _hit, _conditions.hitted());
        At(_move, _kick, _conditions.kick());
        At(_move, _punch, _conditions.punch());

        //Jump Transitions
        At(_jump, _idle, _conditions.grounded());
        At(_jump, _hit, _conditions.hitted());

        //kick Transitions
        At(_kick, _hit, _conditions.hitted());

        //punch Transitions
        At(_punch, _hit, _conditions.hitted());

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