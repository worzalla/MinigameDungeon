using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderDownhill : MonoBehaviour
{
    public GameObject Boulder;
    protected Rigidbody2D body;
    public float slowdown;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("z"))
        {
            body.velocity = body.velocity + new Vector2((float)-1*slowdown, (float)slowdown);
        }
        else
        {
            //body.velocity = body.velocity + new Vector2((float)0.05, (float)-0.05);

        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("Fail");
        }
    }
}
