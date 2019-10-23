using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Put your debug keybinds in here!
 */
public class DevDebug : MonoBehaviour
{
    public GameObject notificationPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // do not run in actual build
        if (!Debug.isDebugBuild)
        {
            Destroy(this);
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Space key creates an example notification for testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(notificationPrefab).GetComponentInChildren<UINotification>().Initialize("Minigame Title");
        }
    }
}
