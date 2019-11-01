using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgingBubble : MonoBehaviour
{
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Minigame.isActive)
        {
            sr.color = new Color(1f, 1f, 1f, Mathf.MoveTowards(sr.color.a, 1f, 0.5f * Time.deltaTime));
            transform.position = Player.GetInstance().transform.position;
        }
        else
        {
            sr.color = new Color(1f, 1f, 1f, Mathf.MoveTowards(sr.color.a, 0f, 1f * Time.deltaTime));
        }
    }
}
