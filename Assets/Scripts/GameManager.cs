using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _gameTime = 20;

    [Header("UI")]
    [SerializeField] private Text _redPointText;
    [SerializeField] private Text _bluePointText;
    [SerializeField] private Text _winner;
    [SerializeField] private GameObject _gameEndingPanel;
    [SerializeField] private ScrollRect _players;

    public static GameManager instance = null;

    private List<GameObject> gameObjects = new List<GameObject>();
    private int _redPoints;
    private int _bluePoints;

    public int RedPoint { get { return _redPoints; } set { _redPoints = value; } }
    public int BluePoint { get { return _bluePoints; } set { _bluePoints = value; } }

    void Start()
    {
        if (instance == null)
        { 
            instance = this;
        }

        else if (instance == this)
        { 
            Destroy(gameObject); // Удаляем объект
        }

        DontDestroyOnLoad(gameObject);
        StartCoroutine(GameTime());

        gameObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Unit"));
    }

    void Update()
    {
        _redPointText.text = _redPoints.ToString();
        _bluePointText.text = _bluePoints.ToString();
    }

    IEnumerator GameTime()
    {
        yield return new WaitForSeconds(_gameTime);
        GetWinner();
    }

    void GetWinner()
    {
        _gameEndingPanel.SetActive(true);

        if(RedPoint > BluePoint)
        {
            _winner.text = "Red command Win!";
        }

        else
        {
            _winner.text = "Blue command Win!";
        }

        foreach(GameObject g in gameObjects)
        {
            Destroy(g);
        }
    }
}
