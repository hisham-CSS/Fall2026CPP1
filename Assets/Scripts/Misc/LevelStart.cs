using UnityEngine;

public class LevelStart : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        GameManager.Instance.SpawnPlayer(transform.position);   
    }
}
