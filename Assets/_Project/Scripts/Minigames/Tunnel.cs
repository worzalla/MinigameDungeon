using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * The tunnel graphic between minigames that the player walks through.
 * Used during the transition between minigames.
 * Cleaned up by the MinigameController.
 */
public class Tunnel : MonoBehaviour
{
    public float alpha;
    public float speed = 1f;
    IEnumerator current;

    SpriteRenderer sr;

    float WALL_PX = 12f;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        // start at 0 alpha, since we fade in
        alpha = 0f;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
    }

    // Update is called once per frame
    void Update()
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
    }

    // position self at bottom corner of previous minigame.
    // return tunnel's bottom right corner position, used by MinigameController to position next minigame
    public SpriteRenderer SetPosition(SpriteRenderer prevMinigameBg)
    {
        if (sr == null)
        {
            sr = GetComponent<SpriteRenderer>();
        }
        // place self at player height
        float tunnelX = prevMinigameBg.transform.position.x + prevMinigameBg.bounds.extents.x + sr.bounds.extents.x;
        float yMax = prevMinigameBg.transform.position.y + prevMinigameBg.bounds.extents.y - sr.bounds.extents.y;
        float yMin = prevMinigameBg.transform.position.y - prevMinigameBg.bounds.extents.y + sr.bounds.extents.y;
        float tunnelY = Mathf.Clamp(Player.GetInstance().transform.position.y, yMin, yMax);
        transform.position = new Vector3(tunnelX, tunnelY, transform.position.z) + GetOffset();
        return sr;
    }

    public Vector3 GetOffset()
    {
        return new Vector3(-(WALL_PX / sr.sprite.pixelsPerUnit), 0f, 0f);
    }

    // tell tunnel to fade to the given opacity.
    public void SetTargetAlpha(float target)
    {
        if (current != null)
        {
            StopCoroutine(current);
        }
        current = TransitionAlpha(target);
        StartCoroutine(current);
    }
    IEnumerator TransitionAlpha(float target)
    {
        while (alpha != target)
        {
            alpha = Mathf.MoveTowards(alpha, target, speed * Time.deltaTime);
            yield return null;
        }
    }
}
