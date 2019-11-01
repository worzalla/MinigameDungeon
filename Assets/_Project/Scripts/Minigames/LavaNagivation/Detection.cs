using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "lava") {
            //Minigame.SetSuccess(false);
            //Minigame.FinishMinigame();
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Finish")
        {
            //Minigame.SetSuccess(true);
            //Minigame.FinishMinigame();
            Destroy(this.gameObject);
        }
        
    }
}
