using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerMinigame : MonoBehaviour
{
    PlatformerMovement movement;
    // Start is called before the first frame update
    void Start()
    {
        Minigame.SetSuccess(false);
        movement = Player.GetInstance()?.GetComponent<PlatformerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // enable physics on player on minigame start
        if (Minigame.isActive)
        {
            Player.GetInstance()?.SetPhysicsActive(true);
            if (movement == null)
            {
                movement = Player.GetInstance().gameObject.AddComponent<PlatformerMovement>();
            }
        }
        // disable physics on player on minigame end
        else
        {
            Player.GetInstance()?.SetPhysicsActive(false);
            if (movement != null)
            {
                Destroy(movement);
            }
        }
    }
}
