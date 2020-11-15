using System;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Fighter))]
public class AILogicManager : MonoBehaviour
{
    //Attacks && actions 
    private Dictionary<string, float> _attacks;
    private Dictionary<string, float> _idleActions;
    private Dictionary<string, float> _moveActions;

    //Private References
    private GameObject _player;
    private Rigidbody2D _rb;
    private Fighter _fighter;
    private float _resetDecision = 0.25f;
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
    private QuestionNode _moveQuestion;

    //ActionNodes
    private ActionNode _attackActionNode;
    private ActionNode _punchActionNode;
    private ActionNode _kickActionNode;
    private ActionNode _moveActionNode;
    private ActionNode _jumpActionNode;

    //Public References
    public GameObject Player { set => _player = value; }
    public QuestionNode IdleQuestion { get => _idleQuestion; }
    public QuestionNode MoveQuestion { get => _moveQuestion; }

    void Start()
    {
        _fighter = gameObject.GetComponent<Fighter>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _stateMachine = new StateMachine();
        _conditions = new AIConditionManager(_fighter);
        _attacks = new Dictionary<string, float>();
        _idleActions = new Dictionary<string, float>();
        _moveActions = new Dictionary<string, float>();

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
        
        //Nodes
        _attackActionNode = new ActionNode(Attack);
        _punchActionNode = new ActionNode(Punch);
        _kickActionNode = new ActionNode(Kick);
        _moveActionNode = new ActionNode(Move);

        _idleQuestion = new QuestionNode(IsInRange, _attackActionNode, _moveActionNode);
        _moveQuestion = new QuestionNode(IsInRange, _attackActionNode, _moveActionNode);

        _attacks.Add("punch", 20);
        _attacks.Add("kick", 10);
        _attacks.Add("back", 10);

        _idleActions.Add("forward", 100);
        _idleActions.Add("backward", 30);
        _idleActions.Add("jump", 0);

        _moveActions.Add("stop", 50);
        _moveActions.Add("continue", 70);
        _moveActions.Add("jump", 0);

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
        if (!_fighter.Jumping)
        {
            string attack = Roulette(_attacks);
            switch (attack)
            {
                case "punch":
                    _punchActionNode.Execute();
                    break;
                case "kick":
                    _kickActionNode.Execute();
                    break;
                case "back":
                    if (_fighter.Fliped) _fighter.Direction = 1;
                    else _fighter.Direction = -1;
                    break;
            }
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
        Debug.Log(_stateMachine.CurrentState);
        if (_stateMachine.CurrentState == _idle)
        {
            string decision = Roulette(_idleActions);
            switch (decision)
            {
                case "forward":
                    if (_fighter.Fliped) _fighter.Direction = -1;
                    else _fighter.Direction = 1;
                    break;
                case "backward":
                    if (_fighter.Fliped) _fighter.Direction = 1;
                    else _fighter.Direction = -1;
                    break;
                case "jump":
                    _fighter.JumpRequest = true;
                    break;
            }
        }
        else if (_stateMachine.CurrentState == _move) 
        {
            if (_decisionTimer > _resetDecision)
            {
                string decision = Roulette(_moveActions);
                switch (decision)
                {
                    case "stop":
                        _fighter.Direction = 0;
                        break;
                    case "continue":
                        break;
                    case "jump":
                        _fighter.JumpRequest = true;
                        break;
                }
                _decisionTimer = 0;
            }
            else 
            {
                _decisionTimer += Time.deltaTime;
            }
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
