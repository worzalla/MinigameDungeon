using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHeartArray : MonoBehaviour
{
    List<Animator> hearts;
    // Start is called before the first frame update
    void Start()
    {
        hearts = new List<Animator>() {
            transform.Find("heart0").gameObject.GetComponent<Animator>(),
            transform.Find("heart1").gameObject.GetComponent<Animator>(),
            transform.Find("heart2").gameObject.GetComponent<Animator>()
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHeartDisplay(int health)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            bool active = i >= (3 - health);
            bool prev = hearts[i].GetBool("Active");
            hearts[i].SetBool("Active", active);
            if (!active && prev)
            {
                hearts[i].gameObject.transform.SetAsLastSibling();
            }
        }
    }
}
