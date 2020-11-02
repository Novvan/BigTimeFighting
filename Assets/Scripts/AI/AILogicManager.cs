using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class AILogicManager : MonoBehaviour
{
    //TODO: DRAW GIZMOS FOR RANGE


    //State
    private enum State { Idle, Punch, Kick, Block, Dash, Jump, Walk, GetHit, Win, Lose };
    [SerializeField] private State _state;

    //Variables
    [SerializeField] private float _speed;
    private float _resetDecision = 2.5f;
    private float _decisionTimer = 0;
    private float _direction;

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

    //Public References
    public GameObject Player { set => _player = value; }
    public float Direction { get => _direction; set => _direction = value; }

    void Start()
    {
        _attacks.Add("Punch", 1.75f);
        _attacks.Add("Kick", 4.5f);
        _attacks.Add("Special", 5f);
        _rb = GetComponent<Rigidbody2D>();
        _state = State.Idle;
        _walk = new ActionNode(Walk);
        _jump = new ActionNode(Jump);
        _kick = new ActionNode(Kick);
        _punch = new ActionNode(Punch);
        _compareHight = new QuestionNode(CompareHeight, _jump, _punch);
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
        _rb.velocity = new Vector2(0, _rb.velocity.y);
        //Roulette punch && kick && sa
        string _decision = "Punch";
        if (IsInRange(_attacks[_decision]))
        {
            //State according to the decision
            _state = State.Punch;
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
        }
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
        _state = State.Walk;
        Vector2 direction = _player.gameObject.transform.position - transform.position;
        direction.y = 0;
        _rb.velocity = new Vector2(direction.normalized.x * _speed, _rb.velocity.y);
    }
}
