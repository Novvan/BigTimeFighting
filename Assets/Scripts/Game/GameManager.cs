using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _player1;
    [SerializeField] private GameObject _ia;
    void Start()
    {
        _player1 = Instantiate(_player1, new Vector3(-5, 0, 0), Quaternion.identity);
        _ia = Instantiate(_ia, new Vector3(5, 0, 0), Quaternion.identity);
        _ia.GetComponent<AILogicManager>().Player = _player1;
    }
}
