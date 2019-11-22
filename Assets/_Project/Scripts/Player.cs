using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum ControlType
{
    TAP, TILT, DRAG, SWIPE, NULL
}

public class Player : MonoBehaviour
{
    public static Player GetInstance()
    {
        return GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
    }

    Rigidbody2D rb;
    Collider2D[] cols;
    [HideInInspector]
    public bool grounded;
    Collider2D vCollider;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cols = GetComponents<Collider2D>();
        SetPhysicsActive(false);
        vCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = true;
        if (!vCollider.isTrigger)
        {
            grounded = Grounded();
        }
        animator.SetBool("Grounded", grounded);
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
            vCollider.bounds.extents.y - vCollider.offset.y + 0.05f);
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

    public static float InputX(ControlType c)
    {
        // arrow key controls on PC
        int arrows = 0;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            arrows--;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            arrows++;
        }
        if (arrows != 0)
        {
            return arrows;
        }
        // mobile controls
        switch (c)
        {
            case ControlType.TILT:
                return Mathf.Clamp(Input.acceleration.x * 4f, -1f, 1f);
            case ControlType.DRAG:
                if (!Input.GetMouseButton(0))
                {
                    return 0f;
                }
                return Mathf.Clamp(0.5f * (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - GetInstance().transform.position.x), -1f, 1f);
        }
        return 0f;
    }

    public static float InputY(ControlType c)
    {
        // arrow key controls on PC
        int arrows = 0;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            arrows++;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            arrows--;
        }
        if (arrows != 0)
        {
            return arrows;
        }
        // mobile controls
        switch (c)
        {
            case ControlType.TILT:
                return Mathf.Clamp(Input.acceleration.y * 4f, -1f, 1f);
            case ControlType.DRAG:
                if (!Input.GetMouseButton(0))
                {
                    return 0f;
                }
                return Mathf.Clamp(0.5f * (Camera.main.ScreenToWorldPoint(Input.mousePosition).y - GetInstance().transform.position.y), -1f, 1f);
            case ControlType.TAP:
                return Input.GetMouseButton(0) ? 1f : 0f;
        }
        return 0f;
    }
}
