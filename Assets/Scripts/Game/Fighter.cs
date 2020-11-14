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
    private bool _hit = false;
    private bool _kickColliderActive = false;
    private bool _punchColliderActive = false;

    public float JumpForce => _jumpForce;
    public float Speed => _speed;
    public float Direction { get => _direction; set => _direction = value; }
    public bool Jumping { get => _jumping; set => _jumping = value; }
    public bool Fliped { get => _fliped; set => _fliped = value; }
    public bool KickColliderActive { get => _kickColliderActive; set => _kickColliderActive = value; }
    public bool PunchColliderActive { get => _punchColliderActive; set => _punchColliderActive = value; }
    public bool Hit { get => _hit; set => _hit = value; }

    private void Awake()
    {
        _kickCollider.SetActive(false);
        _punchCollider.SetActive(false);
    }
    private void Update()
    {
        _kickCollider.SetActive(_kickColliderActive);
        _punchCollider.SetActive(_punchColliderActive);
    }
    private void CheckColliders() 
    {
        _kickCollider.SetActive(_kickColliderActive);
        _punchCollider.SetActive(_punchColliderActive);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 11)
        {
            other.gameObject.GetComponentInParent<Fighter>().Hit = true;
        }
        
    }
}
