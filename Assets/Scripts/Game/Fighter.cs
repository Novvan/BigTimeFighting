using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{

    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;
    [SerializeField] private bool _fliped;
    [SerializeField] private GameObject _kickCollider;
    [SerializeField] private GameObject _punchCollider;


    private float _direction;
    private bool _jumping = false;
    private bool _kickColliderActive;
    private bool _punchColliderActive;

    public float JumpForce => _jumpForce;
    public float Speed => _speed;
    public float Direction { get => _direction; set => _direction = value; }
    public bool Jumping { get => _jumping; set => _jumping = value; }
    public bool Fliped { get => _fliped; set => _fliped = value; }
    public bool KickColliderActive { get => _kickColliderActive; set => _kickColliderActive = value; }
    public bool PunchColliderActive { get => _punchColliderActive; set => _punchColliderActive = value; }

    private void Awake()
    {
        _kickCollider.SetActive(false);
        _punchCollider.SetActive(false);
        _kickColliderActive = false;
        _punchColliderActive = false;
    }
    private void Update()
    {
        CheckColliders();
    }
    private void CheckColliders() 
    {
        if (_kickColliderActive)
        {
            _kickCollider.SetActive(true);
        }
        else
        {
            _kickCollider.SetActive(false);
        }
        if (_punchColliderActive)
        {
            _punchCollider.SetActive(true);
        }
        else
        {
            _punchCollider.SetActive(false);
        }
    }
}
