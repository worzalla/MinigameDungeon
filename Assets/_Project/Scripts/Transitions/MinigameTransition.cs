using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Transition animation between minigames, created by the MinigameController.
 * Uses a sigmoid curve to move the camera.
 * Notifies the MinigameController that the transition is complete using Destroy(gameObject)
 * 
 * TODO: Rotation transitions, where the screen rotates and the player falls
 */
public class MinigameTransition : MonoBehaviour
{
    Vector2 prevPos;
    Vector2 targetPos;

    float prevSize;
    float centerSize;
    float targetSize;

    float time = 0f;
    float duration = 1.5f;

    // if true, make a beeline straight for the goal
    // activated when keyframes finish
    // when we reach the goal, we are done
    bool finish;

    SpriteRenderer prevMinigame;
    GameObject tunnel;
    SpriteRenderer nextMinigame;
    public void Initialize(SpriteRenderer prevMinigame, GameObject tunnel, SpriteRenderer nextMinigame)
    {
        this.prevMinigame = prevMinigame;
        this.tunnel = tunnel;
        this.nextMinigame = nextMinigame;
    }

    // Start is called before the first frame update
    void Start()
    {
        prevPos = Camera.main.transform.position;
        targetPos = nextMinigame.transform.position;
        prevSize = Camera.main.orthographicSize;
        targetSize = GetOrthographicSize(nextMinigame);
        centerSize = 1.5f * 0.5f * (prevSize + targetSize);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        // if we finish animation, make a beeline for target
        if (time > duration)
        {
            Vector2 pos = Vector2.MoveTowards(Camera.main.transform.position, targetPos, 5f * Time.deltaTime);
            Camera.main.transform.position = new Vector3(pos.x, pos.y, Camera.main.transform.position.z);
            Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetSize, 5f * Time.deltaTime);
            if ((Vector2)Camera.main.transform.position == targetPos && Camera.main.orthographicSize == targetSize)
            {
                Destroy(gameObject);
            }
            return;
        }
        // sigmoid curve to target
        Vector2 cameraPos = Vector2.Lerp(prevPos, targetPos, Sigmoid(time / duration, -0.3f));
        Camera.main.transform.position = new Vector3(cameraPos.x, cameraPos.y, Camera.main.transform.position.z);
        float size = Mathf.Lerp(prevSize, centerSize, Sigmoid(2 * time / duration, -0.3f));
        if (2 * time > duration)
        {
            size = Mathf.Lerp(centerSize, targetSize, Sigmoid((2 * time - duration) / duration, -0.3f));
        }
        Camera.main.orthographicSize = size;
    }

    private float GetOrthographicSize(SpriteRenderer bg)
    {
        float aspectRatio = (float)Screen.width / Screen.height;
        return Mathf.Max(bg.bounds.extents.x / aspectRatio, bg.bounds.extents.y);
    }

    // maps [0, 1] to [0, 1] using a sigmoid curve.
    // k = 0 means linear map, otherwise means intensity of velocity smoothing.
    private float Sigmoid(float x, float k)
    {
        float t = Mathf.Lerp(-1f, 1f, x);
        float s = (t - (t * k)) / (k - Mathf.Abs(t) * 2f * k + 1f);
        return Mathf.InverseLerp(-1f, 1f, s);
    }
}
