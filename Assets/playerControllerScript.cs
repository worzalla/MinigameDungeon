using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControllerScript : MonoBehaviour
{
    public GameObject adventurer;
    public int speed = 10;
    private Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            body.velocity = new Vector2(speed * -1, body.velocity.y);
        }
        if (Input.GetKey(KeyCode.D))
        {
            body.velocity = new Vector2(speed, body.velocity.y);
        }
        if (Input.GetKey(KeyCode.S))
        {
            body.velocity = new Vector2(0, speed * -1);
        }
        if (Input.GetKey(KeyCode.W))
        {
            body.velocity = new Vector2(0, speed);
        }
    }
}
