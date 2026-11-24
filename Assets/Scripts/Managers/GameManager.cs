using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton Pattern
    private static GameManager instance;
    public static GameManager Instance => instance;
    #endregion

    #region Lives and Score
    public int maxLives = 9;
    private int _lives = 3;
    public int lives
    {
        get { return _lives; }
        set
        {
            if (value >= maxLives)
            {
                _lives = maxLives;
            }
            else if (value < 0)
            {
                _lives = 0;
                //go to game over
            }
            else if (_lives > value)
            {
                _lives = value;
                //respawn player
            }
            else
            {
                _lives = value;
            }

            Debug.Log($"Lives have changed to {_lives}");
        }
    }

    private int _score = 0;
    public int score
    {
        get { return _score; }
        set
        {
            value = Mathf.Max(0, value);
            _score = value;
        }
    }
    #endregion

    [SerializeField] private PlayerController playerPrefab;
    private PlayerController playerInstance;
    public PlayerController PlayerInstance => playerInstance;

    private Vector3 currentCheckpoint;

    void Awake()
    {
        if (instance == null)
        {
            //we are the first gamemanager object that has appeared in the game
            instance = this;
            DontDestroyOnLoad(this);
            return;
        }

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            string curSceneName = SceneManager.GetActiveScene().name;
            string sceneToLoad = (curSceneName == "Title") ? "Game" : "Title";

            SceneManager.LoadScene(sceneToLoad);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            lives++;
        }
    }

    public void SpawnPlayer(Vector3 pos)
    {
        playerInstance = Instantiate(playerPrefab, pos, Quaternion.identity);
        UpdateCheckpoint(pos);
    }

    public void UpdateCheckpoint(Vector3 newPos)
    {
        currentCheckpoint = newPos;
    }
}