using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDodgeMinigame : MonoBehaviour
{
    Minigame minigame;
    FallDodgeMovement movement;
    // Start is called before the first frame update
    void Start()
    {
        minigame = GetComponent<Minigame>();
        minigame.success = false;
        movement = Player.GetInstance()?.GetComponent<FallDodgeMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // enable physics on player on minigame start
        if (minigame.GetActive())
        {
            Player.GetInstance()?.SetPhysicsActive(true);
            if (movement == null)
            {
                movement = Player.GetInstance().gameObject.AddComponent<FallDodgeMovement>();
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
