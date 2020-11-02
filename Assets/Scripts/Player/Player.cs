using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private StateMachine _stateMachine;



    private void Awake()
    {
        _stateMachine = new StateMachine();
    }

    private void Start()
    {

    }

    private void Update() => _stateMachine.Tick();

}
