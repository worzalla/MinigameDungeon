using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerMinigame : MonoBehaviour
{
    Minigame minigame;
    Rigidbody2D rb;
    Collider2D[] cols;
    // Start is called before the first frame update
    void Start()
    {
        minigame = GetComponent<Minigame>();
        minigame.success = false;
        rb = Player.GetInstance()?.GetComponent<Rigidbody2D>();
        cols = Player.GetInstance()?.GetComponents<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb == null || cols == null)
        {
            rb = Player.GetInstance()?.GetComponent<Rigidbody2D>();
            cols = Player.GetInstance()?.GetComponents<Collider2D>();
        }
        // enable physics on player on minigame start
        if (rb.IsSleeping() && minigame.GetActive())
        {
            rb.WakeUp();
            foreach (Collider2D col in cols)
            {
                col.isTrigger = false;
            }
        }
        // disable physics on player on minigame end
        else if (rb.IsAwake() && !minigame.GetActive())
        {
            rb.Sleep();
            rb.velocity = Vector2.zero;
            foreach (Collider2D col in cols)
            {
                col.isTrigger = true;
            }
        }
    }
}
