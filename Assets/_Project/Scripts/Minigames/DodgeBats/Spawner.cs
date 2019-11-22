using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject tracker;
    Vector2 SpawnPoint;
    public float spawnrate = 1f;
    public float next = 0.0f;
    float randX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Minigame.isActive)
        {
            return;
        }
        if (Time.time > next)
        {
            next = Time.time + spawnrate;
            randX = Random.Range(-8.4f, 8.4f);
            SpawnPoint = new Vector2(transform.position.x + randX, transform.position.y);
            GameObject go = Instantiate(tracker, SpawnPoint, Quaternion.identity);
            go.transform.parent = transform;
        }
    }
}
