using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    public static Player GetInstance()
    {
        return GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
    }

    Rigidbody2D rb;
    Collider2D[] cols;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cols = GetComponents<Collider2D>();
        SetPhysicsActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPhysicsActive(bool active)
    {
        // sleep rigidbody, set to kinematic and disable colliders
        if (!active)
        {
            rb.velocity = Vector2.zero;
        }
        if (active)
        {
            rb.WakeUp();
        }
        else
        {
            rb.Sleep();
        }
        rb.isKinematic = !active;
        foreach (Collider2D col in cols)
        {
            col.isTrigger = !active;
        }
    }
    public bool GetPhysicsActive()
    {
        return rb.IsAwake() && !rb.isKinematic && cols.All(x => !x.isTrigger);
    }
}
