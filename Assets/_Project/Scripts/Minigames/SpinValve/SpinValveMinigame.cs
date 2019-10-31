using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinValveMinigame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Minigame.SetSuccess(false);
    }

    // Update is called once per frame
    void Update()
    {
        // enable physics on player on minigame start
        if (Minigame.isActive)
        {
            Player.GetInstance()?.SetPhysicsActive(true);
        }
        // disable physics on player on minigame end
        else
        {
            Player.GetInstance()?.SetPhysicsActive(false);
        }
    }
}
