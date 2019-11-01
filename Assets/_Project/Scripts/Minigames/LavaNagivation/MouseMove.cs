using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 dir;
    private Vector3 MousePos;
    private float mvspeed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dir = (MousePos - transform.position).normalized;
            rb.velocity = new Vector2(dir.x * mvspeed, dir.y * mvspeed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "lava")
        {
            Minigame.SetSuccess(false);
            Minigame.FinishMinigame();
            //Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Finish")
        {
            Minigame.SetSuccess(true);
            Minigame.FinishMinigame();
            //Destroy(this.gameObject);
        }

    }
}
