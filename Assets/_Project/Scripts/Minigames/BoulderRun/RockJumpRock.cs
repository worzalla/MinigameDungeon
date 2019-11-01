using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockJumpRock : MonoBehaviour
{
    public GameObject Boulder;
    protected Rigidbody2D body;
    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Minigame.isActive)
        {
            body.velocity = Vector2.zero;
            return;
        }
        body.velocity = new Vector2(-1*speed, body.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Minigame.SetSuccess(false);
            Minigame.FinishMinigame();
        }
    }
}
