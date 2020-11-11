using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //Private Varialbes
    [SerializeField] private float _jumpforce;
    [SerializeField] private float _speed;
    private Rigidbody2D _rb;
    private FSM<string> _fsm;
    private AnimationManager _animationManager;
    private float _direction;
    private bool _onfloor;

    //Public Variables
    public float Direction { get => _direction; set =>_direction = value; }
    public bool Onfloor { get => _onfloor; set => _onfloor = value; }
    public float Speed => _speed;
    public float JumpForce => _jumpforce;
    public Rigidbody2D Rigidbody2D => _rb;
    public FSM<string> Fsm => _fsm;
    public AnimationManager AnimationManager => _animationManager;

    //colliders
    [SerializeField] GameObject _punchCollider;
    [SerializeField] GameObject _kickCollider;
    

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animationManager = GetComponent<AnimationManager>();
        _fsm = new FSM<string>();
        IdleState<string> _idle = new IdleState<string>(this);
        MoveState<string> _move = new MoveState<string>(this);
        JumpState<string> _jump = new JumpState<string>(this);
        PunchState<string> _punch = new PunchState<string>(this);
        KickState<string> _kick = new KickState<string>(this);
        _idle.AddTransition("Move", _move);
        _idle.AddTransition("Jump", _jump);
        _idle.AddTransition("Punch", _punch);
        _idle.AddTransition("Kick", _kick);
        _move.AddTransition("Idle", _idle);
        _move.AddTransition("Jump", _jump);
        _move.AddTransition("Punch", _punch);
        _move.AddTransition("Kick", _kick);
        _jump.AddTransition("Idle", _idle);
        _punch.AddTransition("Idle", _idle);
        _kick.AddTransition("Idle", _idle);

        _fsm.SetInit(_idle);
    }

    // Update is called once per frame
    void Update()
    {
        _fsm.OnUpdate();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Environment")) 
        {
            _fsm.Transition("Idle");
            _onfloor = true;
        }
    }

}
