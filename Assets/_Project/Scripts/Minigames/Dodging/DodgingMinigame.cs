using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgingMinigame : MonoBehaviour
{
    DodgingPlayer movement;
    public GameObject arrowPrefab;
    SpriteRenderer bg;
    SpriteRenderer arrowSr;

    // Start is called before the first frame update
    void Start()
    {
        Minigame.SetSuccess(true);
        movement = Player.GetInstance()?.GetComponent<DodgingPlayer>();
        bg = GetComponent<SpriteRenderer>();
        arrowSr = arrowPrefab.GetComponent<SpriteRenderer>();
        // spawn arrows
        StartCoroutine(SpawnArrows());
    }

    // Update is called once per frame
    void Update()
    {
        // enable physics on player on minigame start
        if (Minigame.isActive)
        {
            Player.GetInstance()?.SetPhysicsActive(true);
            if (movement == null)
            {
                movement = Player.GetInstance().gameObject.AddComponent<DodgingPlayer>();
            }
        }
        // disable physics on player on minigame end
        else
        {
            Player.GetInstance()?.SetPhysicsActive(false);
            if (movement != null)
            {
                Destroy(movement);
            }
        }
    }

    IEnumerator SpawnArrows()
    {
        int i = 0;
        while (i < 20)
        {
            if (!Minigame.isActive)
            {
                yield return null;
                continue;
            }
            Rect rect = CameraPosition.CameraRect(arrowSr.bounds.extents);
            float yPos = bg.transform.position.y + (0.8f * Random.Range(-bg.bounds.extents.y, bg.bounds.extents.y));
            float xPos = rect.xMin;
            Vector2 speed = 10f * Vector2.right;
            if (Random.Range(0f, 1f) < 0.5f)
            {
                xPos = rect.xMax;
                speed = 10f * Vector2.left;
            }
            GameObject arrow = Instantiate(arrowPrefab, transform);
            arrow.transform.position = new Vector3(xPos, yPos, transform.position.z);
            arrow.GetComponent<DodgingDestroyer>().speed = speed;
            yield return new WaitForSeconds(0.5f);
            i++;
        }
        yield break;
    }
}
