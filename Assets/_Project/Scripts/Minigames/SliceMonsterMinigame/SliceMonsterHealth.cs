using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceMonsterHealth : MonoBehaviour
{
    float timer = 0f;
    SpriteRenderer sr;
    float invTime = 0.15f;
    int health = 8;
    Explodable ex;
    bool exploded = false;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ex = GetComponent<Explodable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            sr.color = Color.red;
        }
        else
        {
            sr.color = Color.white;
        }
        timer = Mathf.MoveTowards(timer, 0f, Time.deltaTime);
        // if monster is dead, explode
        if (health <= 0 && !exploded)
        {
            Explode();
            exploded = true;
            Minigame.SetSuccess(true);
        }
    }

    // called externally when monster is damaged
    public void Damage()
    {
        if (timer > 0 || health <= 0)
        {
            return;
        }
        timer = invTime;
        health--;
    }

    private void Explode()
    {
        if (ex == null)
        {
            return;
        }
        sr.color = Color.white;
        ex.allowRuntimeFragmentation = true;
        ex.extraPoints = 0;
        ex.subshatterSteps = 1;
        ex.shatterType = Explodable.ShatterType.Voronoi;
        ex.explode();
    }
}
