using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockJumpMinigame : MonoBehaviour
{
    RockJumpPlayer movement;
    // Start is called before the first frame update
    void Start()
    {
        Minigame.SetSuccess(true);
        movement = Player.GetInstance()?.GetComponent<RockJumpPlayer>();
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
                movement = Player.GetInstance().gameObject.AddComponent<RockJumpPlayer>();
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
