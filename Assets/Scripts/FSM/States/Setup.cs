using System.Linq;
using UnityEngine;

public class Setup : IState
{
    private readonly GameObject _entity;

    public Setup(GameObject entity)
    {
        _entity = entity;
    }

    public void OnEnter()
    { }

    public void OnExit()
    { }

    public void Tick()
    { }
}