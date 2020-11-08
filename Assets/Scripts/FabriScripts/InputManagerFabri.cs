using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerFabri : MonoBehaviour
{
    private Fighter _figthter;
    // Start is called before the first frame update
    void Start()
    {
        _figthter = GetComponent<Fighter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) 
        {
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {
                _figthter.Direction = 0;
                _figthter.Fsm.Transition("Idle");
            }
            else if (Input.GetKey(KeyCode.A))
            {
                _figthter.Direction = -1;
                _figthter.Fsm.Transition("Move");
            }
            else 
            {
                _figthter.Direction = 1;
                _figthter.Fsm.Transition("Move");
            }
        }
        else
        {
            _figthter.Direction = 0;
            if (_figthter.Onfloor) _figthter.Fsm.Transition("Idle");
        }
        if (Input.GetButtonDown("Jump")) 
        {
            _figthter.Fsm.Transition("Jump");
        }
    }
}
