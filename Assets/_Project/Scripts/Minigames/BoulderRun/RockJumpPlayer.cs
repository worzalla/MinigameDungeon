using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockJumpPlayer : MonoBehaviour
{
    public GameObject pikachu;
    protected Rigidbody2D body;
    public float force = 9f;
    public float initialY;
    // Start is called before the first frame update
    void Start()
    {
        initialY = transform.position.y;
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Minigame.isActive)
        {
            body.velocity = Vector2.zero;
            return;
        }
        if (Player.InputY(ControlType.TAP) > 0)
        {
            if (Mathf.Abs(body.velocity.y) < 0.01f && transform.position.y <= initialY)
            {
                body.velocity = Vector2.up * force;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Goal")
        {
            Debug.Log("YEET");
        }
    }
}
