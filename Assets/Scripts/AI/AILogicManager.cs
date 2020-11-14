using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Fighter))]
public class AILogicManager : MonoBehaviour
{
    //TODO: DRAW GIZMOS FOR RANGE

    //Variables
    private float _resetDecision = 2.5f;
    private float _decisionTimer = 0;

    //Attacks && actions 
    private Dictionary<string, float> _attacks = new Dictionary<string, float>();

    //Private References
    private GameObject _player;
    private Rigidbody2D _rb;
    private Fighter _fighter;
    private StateMachine _stateMachine;
    private IState _idle;
    private IState _move;
    private IState _jump;
    private IState _hit;
    private IState _kick;
    private IState _punch;

    //Public References
    public GameObject Player { set => _player = value; }

    void Start()
    {
        _fighter = gameObject.GetComponent<Fighter>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _stateMachine = new StateMachine();

        _idle = new Idle(gameObject);
        _move = new Move(gameObject);
        _jump = new Jump(gameObject);
        _hit = new Hit(gameObject);
        _kick = new Kick(gameObject);
        _punch = new Punch(gameObject);

        //idle Transitions
        At(_idle, _move, _false());
        At(_idle, _jump, _false());
        At(_idle, _hit, _hited());
        //At(_idle, _kick, );
        //At(_idle, _punch, );

        //Move transitions
        At(_move, _idle, _false());
        At(_move, _jump, _false());
        At(_move, _hit, _hited());
        //At(_move, _kick, );
        //At(_move, _punch, );

        //Jump Transitions
        At(_jump, _idle, _grounded());

        //kick Transitions
        At(_kick, _hit, _hited());

        //punch Transitions
        At(_punch, _hit, _hited());

        //Custom Conditions
        Func<bool> _grounded() => () =>
        {
            return !_fighter.Jumping;
        };
        Func<bool> _false() => () =>
        {
            return false;
        };
        Func<bool> _hited() => () =>
        {
            return _fighter.Hit;
        };

        //AddTransition alias
        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

        _stateMachine.SetState(_idle);
    }
    bool IsInRange(float d)
    {
        Vector2 _difference = _player.gameObject.transform.position - transform.position;
        _difference.y = 0;
        float _distance = _difference.magnitude;
        if (_distance > d) return false;
        else return true;
    }
    bool CompareHeight()
    {
        return true;
    }
    void Update()
    {
        Debug.Log(_stateMachine.CurrentState);
        _stateMachine.Tick();
        //Roulette punch && kick && sa
        /*string _decision = "Punch";
        if (IsInRange(_attacks[_decision]))
        {
            //State according to the decision
            Debug.Log("Punching");
        }
        else
        {
            if (_decisionTimer >= _resetDecision)
            {
                //Apply roulette
                _decisionTimer = 0;
            }
            else
            {
                _walk.Execute();
                _decisionTimer += Time.deltaTime;
            }
        }*/
    }
    
    public void ResetState()
    {
        _stateMachine.SetState(_idle);
    }
}
