using System;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Fighter))]
public class AILogicManager : MonoBehaviour
{
    //Attacks && actions 
    private Dictionary<string, float> _attacks;
    private Dictionary<string, float> _idleMovements;

    //Private References
    private GameObject _player;
    private Rigidbody2D _rb;
    private Fighter _fighter;
    private float _resetDecision = 2.5f;
    private float _decisionTimer = 0;
    [SerializeField] private float _minAttackDistance; 

    //STATES
    private StateMachine _stateMachine;
    private AIConditionManager _conditions;
    private IState _idle;
    private IState _move;
    private IState _jump;
    private IState _hit;
    private IState _kick;
    private IState _punch;

    //Question Nodes
    private QuestionNode _idleQuestion;

    //ActionNodes
    private ActionNode _attackAction;
    private ActionNode _punchAction;
    private ActionNode _kickAction;
    private ActionNode _moveAction;
    private ActionNode _jumpAction;

    //Public References
    public GameObject Player { set => _player = value; }
    public QuestionNode IdleQuestion { get => _idleQuestion; }
    void Start()
    {
        _fighter = gameObject.GetComponent<Fighter>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _stateMachine = new StateMachine();
        _conditions = new AIConditionManager(_fighter);
        _attacks = new Dictionary<string, float>();

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
        At(_idle, _kick, _conditions.kick());
        At(_idle, _punch, _conditions.punch());

        //Move transitions
        At(_move, _idle, _conditions.falseReturn());
        At(_move, _jump, _conditions.falseReturn());
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
        
        //Nodes
        _attackAction = new ActionNode(Attack);
        _punchAction = new ActionNode(Punch);
        _kickAction = new ActionNode(Kick);
        _moveAction = new ActionNode(Move);

        _idleQuestion = new QuestionNode(IsInRange,_attackAction,_moveAction);
        //_moveQuestion

        _attacks.Add("punch", 20);
        _attacks.Add("kick", 10);

        _idleMovements.Add("idle", 10);
        _idleMovements.Add("forward", 50);
        _idleMovements.Add("backward", 50);
        _idleMovements.Add("jump", 20);

        //AddTransition alias
        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

        _stateMachine.SetState(_idle);
    }
    bool IsInRange()
    {
        Vector2 _difference = _player.gameObject.transform.position - transform.position;
        _difference.y = 0;
        float _distance = _difference.magnitude;
        if (_distance > _minAttackDistance) return false;
        else return true;
    }
    bool CompareHeight()
    {
        return true;
    }
    void Update()
    {
       _stateMachine.Tick();
    }
    void Attack() 
    {
        string attack = Roulette(_attacks);
        switch (attack) 
        {
            case "punch":
                _punchAction.Execute();
                break;
            case "kick":
                _kickAction.Execute();
                break;
        }
    }
    void Punch() 
    {
        _fighter.PunchRequest = true;
    }
    void Kick() 
    {
        _fighter.KickRequest = true;
    }
    void Move() 
    {
        string decision = Roulette(_idleMovements);
        switch (decision) 
        {
            case "idle":
                break;
            case "forward":
                break;
            case "backward":
                break;
            case "jump":
                break;
        }
    }
    string Roulette(Dictionary<string,float> dic) 
    {
        float _totalProbabilitie = 0;
        foreach (var option in dic) 
        {
            _totalProbabilitie += option.Value;
        }
        float random = UnityEngine.Random.Range(0, _totalProbabilitie);
        foreach (var option in dic) 
        {
            random -= option.Value;
            if (random <= 0) 
            {
                return option.Key;
            }
        }
        return null;
    }

    public void ResetState()
    {
        _stateMachine.SetState(_idle);
    }
}
