using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _ia;
    [SerializeField] private Scene currentScene = Scene.menu;

    private float difference;

    public GameObject Ia { get => _ia; set => _ia = value; }
    public GameObject Player { get => _player; set => _player = value; }
    private enum Scene { menu, fight }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (currentScene == Scene.fight)
        {
            _player = Instantiate(_player, new Vector3(-5, 0, 0), Quaternion.identity);
            _player.tag = "Player";

            _ia = Instantiate(_ia, new Vector3(5, 0, 0), Quaternion.identity);
            _ia.GetComponent<AILogicManager>().Player = _player;
            _ia.tag = "Enemy";
        }
    }
    void Start()
    {
        Debug.Log(currentScene.ToString());

    }
    private void Update()
    {
        if (currentScene == Scene.fight)
        {
            difference = _ia.gameObject.transform.position.x - _player.gameObject.transform.position.x;
            if (difference > 0)
            {
                _player.GetComponent<Fighter>().Fliped = false;
                _ia.GetComponent<Fighter>().Fliped = true;
            }
            else
            {
                _player.GetComponent<Fighter>().Fliped = true;
                _ia.GetComponent<Fighter>().Fliped = false;
            }
        }
    }
    public void StartFight()
    {
        Debug.Log("state Change");
        currentScene = Scene.fight;
    }
}
