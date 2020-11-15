using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Fighter))]
public class AIFighter : MonoBehaviour
{
    private StateMachine _stateMachine;
    private Fighter _fighter;
    private Rigidbody2D _rb;
    private AIConditionManager _conditions;
    private IState _idle;
    private IState _move;
    private IState _jump;
    private IState _hit;
    private IState _kick;
    private IState _punch;

    public IState IdleState { get => _idle; }
    public IState MoveState { get => _move; }
    public Fighter Fighter { get => _fighter; }
    public StateMachine StateMachine { get => _stateMachine; }

    void Start()
    {
        _fighter = gameObject.GetComponent<Fighter>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _stateMachine = new StateMachine();
        _conditions = new AIConditionManager(_fighter);

        //States
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

    void Update()
    {
        _stateMachine.Tick();
    }
}
