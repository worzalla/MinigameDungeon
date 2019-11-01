using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    int enemiescount;
    // Start is called before the first frame update
    void Start()
    {
        Minigame.SetSuccess(false);

        enemiescount = 3;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiescount = enemies.Length;
        if (enemiescount == 0)
        {
            Minigame.SetSuccess(true);
            Minigame.FinishMinigame();
        }
    }
}
