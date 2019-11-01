using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgingPlayer : MonoBehaviour
{
    public GameObject adventurer;
    public float speed = 10;
    private Rigidbody2D body;
    float acc = 20f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.drag = 0.1f;
        body.gravityScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Minigame.isActive)
        {
            body.velocity = Vector2.zero;
            return;
        }
        int xDirection = 0;
        int yDirection = 0;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            xDirection--;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            xDirection++;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            yDirection--;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            yDirection++;
        }
        body.velocity = Vector2.MoveTowards(body.velocity, new Vector2(xDirection * speed, yDirection * speed), acc * Time.deltaTime);
    }

    void OnDestroy()
    {
        body.drag = 0f;
        body.gravityScale = 1f;
    }
}
