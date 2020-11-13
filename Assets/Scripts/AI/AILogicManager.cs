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
    private ActionNode _walk;
    private ActionNode _jump;
    private ActionNode _punch;
    private ActionNode _kick;
    private QuestionNode _compareHight;

    //Private References
    private GameObject _player;
    private Rigidbody2D _rb;
    private Fighter _fighter;
    private StateMachine _stateMachine;

    //Public References
    public GameObject Player { set => _player = value; }

    void Start()
    {
        _fighter = this.gameObject.GetComponent<Fighter>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _stateMachine = new StateMachine();

        var idle = new Idle(gameObject);
        var move = new Move(gameObject);
        var jump = new Jump(gameObject);
        
        //idle Transitions
        At(idle, move, _true());
        At(idle, jump, _true());
        //At(idle, punck, _inputManager.punch())
        //Move transitions
        At(move, idle, _true());
        At(move, jump, _true());
        //Jump Transitions
        At(jump, idle, _grounded());
        
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

        _stateMachine.SetState(idle);
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
    void Punch()
    {

    }
    void Kick()
    {

    }
    void Jump()
    {

    }
    void Walk()
    {
        
    }
}
