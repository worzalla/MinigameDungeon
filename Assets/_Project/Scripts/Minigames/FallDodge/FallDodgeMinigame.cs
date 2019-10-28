﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDodgeMinigame : MonoBehaviour
{
    Minigame minigame;
    FallDodgeMovement movement;
    SpriteRenderer background;
    SpriteRenderer cameraSize;

    // Start is called before the first frame update
    void Start()
    {
        minigame = GetComponent<Minigame>();
        minigame.success = true;
        movement = Player.GetInstance()?.GetComponent<FallDodgeMovement>();
        background = transform.Find("Background").GetComponent<SpriteRenderer>();
        cameraSize = GetComponent<SpriteRenderer>();
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
            // control camera
            MoveCameraSmooth(Player.GetInstance().transform.position.y - 3f);
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

    private void MoveCameraSmooth(float y)
    {
        // follow the specified object
        // to smooth the following, use a differential equation
        // where acceleration makes spd approach targetSpd = k1 * distanceToTravel
        // acceleration is k2 * spdDifference
        float targetPos = Mathf.Clamp(y,
            background.bounds.center.y - background.bounds.extents.y + cameraSize.bounds.extents.y,
            background.bounds.center.y + background.bounds.extents.y - cameraSize.bounds.extents.y);
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,
            targetPos, Camera.main.transform.position.z);
    }
}
