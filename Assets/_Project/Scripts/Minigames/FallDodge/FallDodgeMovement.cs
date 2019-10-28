using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDodgeMovement : MonoBehaviour
{
    Rigidbody2D rb;
    float maxSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            // fail minigame
            Minigame.SetSuccess(false);
            Minigame.FinishMinigame();
        }
    }
}
