using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int hp = 1;
    // Start is called before the first frame update
    public void Hit(int damage)
    {
        hp -= 1;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
