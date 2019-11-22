using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    private Vector3 aim;
    public GameObject bow;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!Minigame.isActive)
        {
            return;
        }
        aim = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        Vector3 calc = aim - bow.transform.position;
        float Rotation = Mathf.Atan2(calc.y, calc.x) * Mathf.Rad2Deg;
        bow.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Rotation);
    }
}
