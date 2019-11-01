using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanics : MonoBehaviour
{
    public float Speed = 16;
    public Rigidbody2D RigidBody1;
    // Start is called before the first frame update
    void Start()
    {
        RigidBody1.velocity = transform.right * Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        Target target = collision.GetComponent<Target>();
        if (target != null)
        {
            target.Hit(1);
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
}
