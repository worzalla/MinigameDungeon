using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPlayer : MonoBehaviour
{
    Adventurer movement2;
    // Start is called before the first frame update
    void Start()
    {
        Minigame.SetSuccess(true);
        movement2 = Player.GetInstance()?.GetComponent<Adventurer>();
    }

    // Update is called once per frame
    void Update()
    {
        // enable physics on player on minigame start
        if (Minigame.isActive)
        {
            Player.GetInstance()?.SetPhysicsActive(true);
            if (movement2 == null)
            {
                movement2 = Player.GetInstance().gameObject.AddComponent<Adventurer>();
            }
        }
        // disable physics on player on minigame end
        else
        {
            Player.GetInstance()?.SetPhysicsActive(false);
            if (movement2 != null)
            {
                Destroy(movement2);
            }
        }
    }
}
