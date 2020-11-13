﻿using System;
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

    //Public References
    public GameObject Player { set => _player = value; }

    void Start()
    {
        _fighter = this.gameObject.GetComponent<Fighter>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _stateMachine = new StateMachine();

        _idle = new Idle(gameObject);
        _move = new Move(gameObject);
        _jump = new Jump(gameObject);
        
        //idle Transitions
        At(_idle, _move, _true());
        At(_idle, _jump, _true());
        //At(idle, punck, _inputManager.punch())
        //Move transitions
        At(_move, _idle, _true());
        At(_move, _jump, _true());
        //Jump Transitions
        At(_jump, _idle, _grounded());
        
        //Custom Conditions
        Func<bool> _grounded() => () =>
        {
            return !_fighter.Jumping;
        };
        Func<bool> _true() => () =>
        {
            return true;
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
