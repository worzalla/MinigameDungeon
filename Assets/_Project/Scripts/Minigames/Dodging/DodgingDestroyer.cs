using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgingDestroyer : MonoBehaviour
{
    public GameObject destroyer;
    public Vector2 speed = Vector2.right * 10f;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        transform.eulerAngles = new Vector3(0f, 0f, Vector2.SignedAngle(Vector2.right, speed));
    }

    // Update is called once per frame
    void Update()
    {
        if (Minigame.isActive)
        {
            transform.position += (Vector3)speed * Time.deltaTime;
        }
        //check for destruction
        if (!CameraPosition.CameraRect(sr.bounds.extents).Contains(transform.position))
        {
            Destroy(gameObject);
        }
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
