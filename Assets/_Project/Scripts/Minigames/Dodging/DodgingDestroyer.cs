using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgingDestroyer : MonoBehaviour
{
    public GameObject destroyer;
    public int speed = 10;
    private Rigidbody2D body;
    float x;
    float y;
    Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        int rand = Random.Range(1, 3);
        if(rand == 1)
        {
            x = -11;
            y = Random.Range(-3, 4);
        }
        if(rand == 2)
        {
            x = 11;
            y = Random.Range(-3, 4);
            speed = speed * -1;
        }
        pos= new Vector2(x, y);
        transform.position = pos;
        
    }

    // Update is called once per frame
    void Update()
    {

        if(transform.position.x > 11 || transform.position.x < -11)
        {
            int rand = Random.Range(1, 3);
            if (rand == 1)
            {
                x = -11;
                y = Random.Range(-3, 4);
            }
            if (rand == 2)
            {
                x = 11;
                y = Random.Range(-3, 4);
                speed = speed * -1;
            }
            pos = new Vector2(x, y);
            transform.position = pos;
        }
        body.velocity = new Vector2(speed, 0);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Minigame.SetSuccess(false);
            Minigame.FinishMinigame();
        }
    }
}
