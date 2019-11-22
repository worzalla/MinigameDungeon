using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Minigame GameObject requirements:
 * 
 * Minigames are GameObject prefabs that are placed into the Minigames field on the MinigameController (using the Unity Editor).
 * 
 * Minigames must contain a Minigame component (this class), since it notifies the MinigameController of its title and
 * allows the Minigame to notify the MinigameController when it is complete. It also notifies the
 * MinigameController of its success/failure status.
 * 
 * Minigames must also contain an object with a SpriteRenderer tagged MinigameBackground. This is so that
 * the MinigameController can correctly scroll the screen to fit the Minigame.
 * 
 * Minigames should consistently check GetActive() to make sure the MinigameController has not deactivated them.
 * This is so that, for example, the player doesn't hit a rock and explode after the minigame has shown "Success!"
 * 
 * Minigame should contain an initial player position specifier so that
 * the MinigameController can move the player to its position in the minigame during the transition
 */

// common functionality to all minigames here
public class Minigame : MonoBehaviour
{
    [Space(10)]

    [Header("One GameObject must contain the tag MinigameBackground and have a SpriteRenderer")]
    [Header("Title field in the Minigame component must be set")]
    [Header("One GameObject must contain this Minigame component")]
    [Header("Minigame GameObject requirements:")]

    public string title;
    // read by MinigameController to determine pass/fail
    public bool success = true;

    // read by UIController to determine what to show. This should probably be an enum but ehh.
    public string gestureType = "";

    // don't do anything unless set as activate by MinigameController
    // inactive includes before message has disappeared or after player has won or during transitions
    [HideInInspector]
    public bool active = false;

    static Minigame minigame;

    private void Awake()
    {
        minigame = this;
        active = false;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Minigame GetInstance()
    {
        return minigame;
    }
    public static bool isActive { get { return GetInstance() == null ? false : GetInstance().GetActive(); } }
    public static void SetSuccess(bool value)
    {
        GetInstance().success = value;
    }

    public static void FinishMinigame() 
    {
        GameObject.FindGameObjectWithTag("MinigameController").GetComponent<MinigameController>().FinishMinigame();
    }

    // should only be called by MinigameController
    public void Enable()
    {
        active = true;
    }
    public void Disable()
    {
        active = false;
    }

    // should be used by minigame
    // don't do anything if inactive
    public bool GetActive()
    {
        return active;
    }
}
