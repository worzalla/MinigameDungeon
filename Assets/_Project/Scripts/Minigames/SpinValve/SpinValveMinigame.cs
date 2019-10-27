using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinValveMinigame : MonoBehaviour
{
    Minigame minigame;
    // Start is called before the first frame update
    void Start()
    {
        minigame = GetComponent<Minigame>();
        minigame.success = false;
    }

    // Update is called once per frame
    void Update()
    {
        // enable physics on player on minigame start
        if (minigame.GetActive())
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
