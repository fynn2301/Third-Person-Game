using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public float waitTime = 5f;
    public GameObject spawnPoint;
    public GameObject enemy1;
    public bool waveStart = true;
    // Start is called before the first frame update
    float startTime;
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > startTime + waitTime)
        {
            Instantiate(enemy1, spawnPoint.transform.position, spawnPoint.transform.rotation);
            waitTime = 1f;
            startTime = Time.time;
        }
    }
}
