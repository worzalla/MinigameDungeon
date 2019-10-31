using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDodgeHazard : MonoBehaviour
{
    SpriteRenderer sr;
    float speed = 90f;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles += Vector3.forward * speed * Time.deltaTime;
    }
}
