using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerMovement : MonoBehaviour
{
    public float horizontalSpeed = 3f;
    public float jumpSpeed = 5f;
    private float gravity = 0.5f;
    private Sprite[] gasSprites;

    Animator animator;
    Rigidbody2D body;
    SpriteRenderer sr;
    bool grounded;
    Player player;

    [HideInInspector] public int facingX = -1;

    void Start()
    {
        gravity = 0.9f;
        gasSprites = Resources.LoadAll<Sprite>("gasMode");
        sr = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = Player.GetInstance();
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
        grounded = player.grounded;

        // can't move if transition exists
        float hDirection = Player.InputX(ControlType.TILT);
        body.velocity = new Vector2(hDirection * horizontalSpeed, body.velocity.y);

        if (Player.InputY(ControlType.TAP) > 0f && grounded)
        {
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        }

        // animate with blend tree
        animator.SetInteger("VelocityX", (int)Mathf.Sign(hDirection));
        if (Mathf.Sign(hDirection) != 0)
        {
            animator.SetInteger("FacingX", (int)Mathf.Sign(hDirection));
            facingX = (int)Mathf.Sign(hDirection);
            sr.flipX = facingX < 0;
        }
        animator.SetInteger("VelocityY", (int)Mathf.Sign(body.velocity.y));
    }

    // reset animator on destroy
    void OnDestroy()
    {
        animator.SetInteger("VelocityX", 0);
        animator.SetInteger("FacingX", 0);
        animator.SetInteger("VelocityY", 0);
    }

    bool Movable()
    {
        return true;
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