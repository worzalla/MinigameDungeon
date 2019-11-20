using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner2: MonoBehaviour
{
    public GameObject tracker;
    Vector2 SpawnPoint;
    public float spawnrate = 1f;
    public float next = 0.0f;
    float randY;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > next)
        {
            next = Time.time + spawnrate;
            randY = Random.Range(-3.7f, 4f);
            SpawnPoint = new Vector2(transform.position.x, randY);
            GameObject go = Instantiate(tracker, SpawnPoint, Quaternion.identity);
            go.transform.parent = transform;
        }
    }
}
