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
    public Vector3 SetPosition(SpriteRenderer prevMinigameBg)
    {
        if (sr == null)
        {
            sr = GetComponent<SpriteRenderer>();
        }
        // place self between prev and next minigames at bottom
        Vector2 prevBottomRight = (Vector2)prevMinigameBg.transform.position + new Vector2(1f, -1f) * prevMinigameBg.bounds.extents + (Vector2)sr.bounds.extents;
        transform.position = new Vector3(prevBottomRight.x - (WALL_PX / sr.sprite.pixelsPerUnit), prevBottomRight.y, transform.position.z);
        return transform.position + (Vector3)(new Vector2(1f, -1f) * sr.bounds.extents) + Vector3.left * (WALL_PX / sr.sprite.pixelsPerUnit);
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
