using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDodgeMovement : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    float maxSpeed = 5f;
    float horizontalSpeed = 5f;
    float tilt = 15f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Minigame.isActive)
        {
            sr.flipX = false;
            transform.localEulerAngles = Vector3.zero;
            rb.velocity = Vector2.zero;
            return;
        }
        rb.drag = 0f;

        // can't move if transition exists
        int hDirection = 0;
        if (Input.GetKey("left"))
        {
            hDirection--;
        }
        if (Input.GetKey("right"))
        {
            hDirection++;
        }
        transform.localEulerAngles = Vector3.forward * Mathf.Clamp(hDirection, -1f, 1f) * tilt;
        rb.velocity = new Vector2(hDirection * horizontalSpeed, rb.velocity.y);
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Hazard")
        {
            // fail minigame
            Minigame.SetSuccess(false);
            Minigame.FinishMinigame();
        }
    }
}
