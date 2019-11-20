using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trackers : MonoBehaviour
{
    public Transform player;
    private Rigidbody2D rb;
    public float movementspeed = 80f;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.GetInstance().transform;
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //rb.rotation = angle;
        direction.Normalize();
        movement = direction;


    }
    private void FixedUpdate()
    {
        rb.MovePosition((Vector2)transform.position + (movement * movementspeed * Time.deltaTime) * 9/4);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        //if (collision.gameObject.tag == "lava")
        //{
            //Minigame.SetSuccess(false);
            //Minigame.FinishMinigame();
          //  Destroy(this.gameObject);
        //}
        if (collision.gameObject.tag == "Player")
        {
            Minigame.SetSuccess(false);
            Minigame.FinishMinigame();
            //Minigame.SetSuccess(true);
            //Minigame.FinishMinigame();
            //Destroy(this.gameObject);
        }

    }
}