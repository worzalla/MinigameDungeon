using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adventurer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    Rigidbody2D RigidBody;
    bool DirectionRight;
    float speed = 6.0f;
    void Start()
    {
        DirectionRight = true;
        RigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKey(KeyCode.A))
        {
            RigidBody.velocity = new Vector2(-3, RigidBody.velocity.y);
            DirectionRight = false;
            //FlipLeft();
            Flip();
        }
      */
        if (Input.GetKeyDown(KeyCode.W))
        {
            RigidBody.velocity = new Vector2(0, 7);
        }
        /*
        if (Input.GetKey(KeyCode.D))
        {
            RigidBody.velocity = new Vector2(3, RigidBody.velocity.y);
            DirectionRight = true;
            //FlipRight();
            Flip();
        }
        */
        float move = Input.GetAxis("Horizontal");
        if (move < 0) RigidBody.velocity = new Vector3(move * speed, RigidBody.velocity.y);
        if (move > 0) RigidBody.velocity = new Vector3(move * speed, RigidBody.velocity.y);

        if (move < 0 && DirectionRight) Flip();
        if (move > 0 && !DirectionRight) Flip();
    }
    /*
    private void FlipRight()
    {
        if (DirectionRight == true){
            Vector3 scale = transform.localScale;
            scale.x = -1;
            transform.localScale = scale;
        }
    }
    private void FlipLeft()
    {
        if (DirectionRight == false)
        {
            Vector3 scale = transform.localScale;
            scale.x = 1;
            transform.localScale = scale;
        }
    }
    */
    private void Flip()
    {
        DirectionRight = !DirectionRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
