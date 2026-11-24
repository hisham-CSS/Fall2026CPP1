using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject[] pickups;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int randNum = Random.Range(0, pickups.Length);

        if (pickups[randNum] == null) return;

        Instantiate(pickups[randNum], transform.position, Quaternion.identity);
    }
}
