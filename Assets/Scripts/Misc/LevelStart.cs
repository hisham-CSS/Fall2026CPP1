using UnityEngine;

public class LevelStart : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.SpawnPlayer(transform.position);   
    }
}


//Awake happens before start specifically meant for the observer pattern and events - and setup stuff that needs to happen before any start function happens.