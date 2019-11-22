using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform point;
    public GameObject bullet;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (!Minigame.isActive)
        {
            return;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }
    void Fire()
    {
        Instantiate(bullet, point.position, point.rotation);
    }
}
