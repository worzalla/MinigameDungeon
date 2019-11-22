using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trackers : MonoBehaviour
{
    public Transform player;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.GetInstance().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Minigame.isActive)
        {
            return;
        }
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        //if (collision.gameObject.tag == "lava")
        //{
            //Minigame.SetSuccess(false);
            //Minigame.FinishMinigame();
          //  Destroy(this.gameObject);
        //}
        if (collision.gameObject.tag == "Player")
        {
            Minigame.SetSuccess(false);
            Minigame.FinishMinigame();
            //Minigame.SetSuccess(true);
            //Minigame.FinishMinigame();
            //Destroy(this.gameObject);
        }

    }
}