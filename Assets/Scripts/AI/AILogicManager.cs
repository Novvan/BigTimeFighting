using System;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Fighter))]
public class AILogicManager : MonoBehaviour
{
    //Attacks && actions 
    private Dictionary<string, float> _attacks = new Dictionary<string, float>();

    //Private References
    private GameObject _player;
    private Rigidbody2D _rb;
    private Fighter _fighter;
    private float _resetDecision = 2.5f;
    private float _decisionTimer = 0;

    //STATES
    private StateMachine _stateMachine;
    private AIConditionManager _conditions;
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
        _conditions = new AIConditionManager(_fighter);

        _idle = new Idle(gameObject);
        _move = new Move(gameObject);
        _jump = new Jump(gameObject);
        _hit = new Hit(gameObject);
        _kick = new Kick(gameObject);
        _punch = new Punch(gameObject);

        //idle Transitions
        At(_idle, _move, _conditions.falseReturn());
        At(_idle, _jump, _conditions.falseReturn());
        At(_idle, _hit, _conditions.hitted());
        //At(_idle, _kick, );
        //At(_idle, _punch, );

        //Move transitions
        At(_move, _idle, _conditions.falseReturn());
        At(_move, _jump, _conditions.falseReturn());
        At(_move, _hit, _conditions.hitted());
        //At(_move, _kick, );
        //At(_move, _punch, );

        //Jump Transitions
        At(_jump, _idle, _conditions.grounded());

        //kick Transitions
        At(_kick, _hit, _conditions.hitted());

        //punch Transitions
        At(_punch, _hit, _conditions.hitted());


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
