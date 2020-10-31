using System.Collections;
using UnityEngine;

public abstract class PlayerStateMachine : MonoBehaviour
{
    protected PlayerState State;
    public void SetState(PlayerState state)
    {
        State = state;
    }
}