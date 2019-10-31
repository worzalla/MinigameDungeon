using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceMonsterMinigame : MonoBehaviour
{
    AudioSource dio;
    public AudioClip swish;
    public AudioClip sword1;
    public AudioClip sword2;
    public AudioClip sword3;
    public GameObject cutAnimation;

    // Start is called before the first frame update
    void Start()
    {
        dio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Swipe s = SliceMonsterInput.GetSwipe();
        if (s != null)
        {
            Instantiate(cutAnimation).GetComponent<CutAnimation>().Initialize(s.start, s.end);
            DamageMonster(s.start, s.end);
        }
    }

    void DamageMonster(Vector2 start, Vector2 end)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(start, (end - start).normalized,
            (end - start).magnitude, LayerMask.GetMask("Enemy"));
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.tag == "Enemy")
            {
                // damage enemy
                hit.collider.gameObject.GetComponent<SliceMonsterHealth>().Damage();
            }
        }
        // play a sound
        if (hits.Length == 0)
        {
            // play miss sound
            dio.PlayOneShot(swish);
        }
        else
        {
            // play sword swipe sound
            int soundSwitch = Random.Range(1, 4);
            switch (soundSwitch)
            {
                case 1: dio.PlayOneShot(sword1); break;
                case 2: dio.PlayOneShot(sword2); break;
                case 3: dio.PlayOneShot(sword3); break;
                default: dio.PlayOneShot(sword3); break;
            }
            dio.Play();
        }
    }

    // clean up fragments on destruction
    void OnDestroy()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Fragment"))
        {
            Destroy(go);
        }
    }
}
