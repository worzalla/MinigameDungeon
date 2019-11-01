using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimBallistaTarget : MonoBehaviour
{
    // move up and down by 2 units
    float initialY;
    int dir = 1;
    float timer = 0f;
    float duration = 1.5f;
    float dist = 3f;

    // Start is called before the first frame update
    void Start()
    {
        initialY = transform.position.y;
        timer = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Minigame.isActive)
        {
            return;
        }
        timer += Time.deltaTime;
        if (timer > duration)
        {
            timer = 0f;
            dir = -dir;
        }
        float yPos = Mathf.Lerp(initialY - (dist * dir), initialY + (dist * dir), timer / duration);
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Projectile")
        {
            Minigame.SetSuccess(true);
            Minigame.FinishMinigame();
        }
    }
}
