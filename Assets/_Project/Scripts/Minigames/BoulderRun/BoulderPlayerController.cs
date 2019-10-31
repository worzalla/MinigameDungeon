using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderPlayerController : MonoBehaviour
{
    public GameObject pikachu;
    protected Rigidbody2D body;
    public int force;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && body.velocity == new Vector2(0,0))
        {
            body.AddForce(new Vector2(0, 2*force));
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
