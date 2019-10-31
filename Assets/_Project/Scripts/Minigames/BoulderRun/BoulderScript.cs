using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderScript : MonoBehaviour
{
    public GameObject Boulder;
    protected Rigidbody2D body;
    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = new Vector2(-1*speed, 0);
    }
}
