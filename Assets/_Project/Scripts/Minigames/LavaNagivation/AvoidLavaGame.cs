using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidLavaGame : MonoBehaviour
{
    MouseMove movement;
    // Start is called before the first frame update
    void Start()
    {
        Minigame.SetSuccess(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Minigame.isActive)
        {
            Player.GetInstance()?.SetPhysicsActive(true);
            if (movement == null)
            {
                movement = Player.GetInstance().gameObject.AddComponent<MouseMove>();
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
