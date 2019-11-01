using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockJumpPlayer : MonoBehaviour
{
    public GameObject pikachu;
    protected Rigidbody2D body;
    public float force = 120f;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("what");
            if (Mathf.Abs(body.velocity.y) < 0.05f)
            {
                body.AddForce(new Vector2(0, 2 * force));
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
