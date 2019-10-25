using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerMovement : MonoBehaviour
{
    public float horizontalSpeed;
    public float jumpSpeed;
    private float gravity;
    private Sprite[] gasSprites;

    Animator animator;
    Rigidbody2D body;
    SpriteRenderer sr;
    Collider2D vCollider;
    bool grounded;

    [HideInInspector] public int facingX = -1;

    void Start()
    {
        gravity = 0.9f;
        gasSprites = Resources.LoadAll<Sprite>("gasMode");
        sr = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        vCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Minigame.isActive)
        {
            return;
        }
        body.gravityScale = gravity;
        body.drag = 0f;
        sr.flipX = false;
        grounded = Grounded();

        // can't move if transition exists
        int hDirection = 0;
        if (Input.GetKey("left") && Movable())
        {
            hDirection--;
        }
        if (Input.GetKey("right") && Movable())
        {
            hDirection++;
        }
        body.velocity = new Vector2(hDirection * horizontalSpeed, body.velocity.y);

        if (Input.GetKeyDown("up") && Jumpable())
        {
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        }

        // animate with blend tree
        //animator.SetFloat("VelocityX", hDirection);
        if (hDirection != 0)
        {
            //animator.SetFloat("FacingX", hDirection);
            facingX = hDirection;
        }
        //animator.SetFloat("VelocityY", Mathf.Sign(body.velocity.y));
        //animator.SetBool("Grounded", grounded);
    }

    bool Jumpable()
    {
        return Grounded();
    }

    bool Movable()
    {
        return true;
    }

    bool Grounded()
    {
        return CheckRaycastGround(Vector2.zero) ||
            CheckRaycastGround(Vector2.left * (vCollider.bounds.extents.x + vCollider.offset.x)) ||
            CheckRaycastGround(Vector2.right * (vCollider.bounds.extents.x + vCollider.offset.x));
    }

    bool CheckRaycastGround(Vector2 pos)
    {
        // player has 2 colliders, so to find more, make this 3
        RaycastHit2D[] results = new RaycastHit2D[3];
        // raycast for a collision down
        Physics2D.Raycast((Vector2)transform.position + pos, Vector2.down, new ContactFilter2D(), results,
            vCollider.bounds.extents.y - vCollider.offset.y + 0.5f);
        // make sure raycast hit isn't only player
        foreach (RaycastHit2D result in results)
        {
            if (result.collider != null)
            {
                if (result.collider.gameObject.tag != "Player")
                {
                    return true;
                }
            }
        }
        return false;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (!Minigame.isActive)
        {
            return;
        }
        if (grounded && col.gameObject.tag == "Goal")
        {
            Minigame.SetSuccess(true);
            Minigame.FinishMinigame();
        }
        if (col.gameObject.tag == "Hazard")
        {
            Minigame.SetSuccess(false);
            Minigame.FinishMinigame();
        }
    }
}