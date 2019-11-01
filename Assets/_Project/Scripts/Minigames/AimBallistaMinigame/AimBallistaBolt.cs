using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimBallistaBolt : MonoBehaviour
{
    GameObject wall;
    Vector2 speed = Vector2.zero;
    float angle;
    bool launched = false;
    Rigidbody2D rb;
    SpriteRenderer sr;

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
            rb.velocity = Vector2.zero;
            return;
        }
        if (launched)
        {
            rb.velocity = speed;
            transform.eulerAngles = new Vector3(0f, 0f, angle);
        }
        // check for out of bounds
        Rect rect = CameraPosition.CameraRect(sr.bounds.extents);
        if (!rect.Contains(transform.position))
        {
            Minigame.SetSuccess(false);
            Minigame.FinishMinigame();
        }
    }
    
    public void Launch(Vector2 speed, float angle)
    {
        this.speed = speed;
        this.angle = angle;
        transform.parent = transform.parent.parent;
        launched = true;
    }
}
