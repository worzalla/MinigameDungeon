using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Specifies where to put the player in a minigame when the minigame starts.
 * 
 * Each minigame must contain one of these.
 */
public class PlayerStartingPosition : MonoBehaviour
{
    public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // if no player exists, create one
        if (Player.GetInstance() == null)
        {
            Instantiate(playerPrefab, transform.position, transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
