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
                GameOver();
            }
            else if (_lives > value)
            {
                _lives = value;
                Respawn();
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

    //EVENT DRIVEN PROGRAMMING: paradigm which allows the flow of the program to be determined by events rather than a linear sequence of instructions. Allows for more flexibility and responsiveness in a program as it can react to events as they happen rather than waiting for a specific point in code to execute.  Events are the backbone of the observer pattern.

    //Observer pattern: The subject (object) maintains a list of dependants that will be notified once a specfic event occurs.  This can decouple the caller from the objects requiring it because the subject doesn't need to care about what the dependants are doing.

    //delegates are a type that represents a method or a function with a specific parameter list.

    public delegate void PlayerStateDelegate(PlayerController player);
    public event PlayerStateDelegate OnPlayerSpawned;
    public event PlayerStateDelegate OnPlayerRespawned;

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
        OnPlayerSpawned?.Invoke(playerInstance);
    }

    public void UpdateCheckpoint(Vector3 newPos)
    {
        currentCheckpoint = newPos;
    }

    private void GameOver()
    {
        Debug.Log("Game Over Triggered");
        //game over scene switching logic can happen here
    }

    private void Respawn()
    {
        playerInstance.transform.position = currentCheckpoint;
        OnPlayerRespawned?.Invoke(playerInstance);
    }
}