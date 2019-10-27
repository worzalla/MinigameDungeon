using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinValveWater : MonoBehaviour
{
    public float targetHeight = 0.9f;
    public float height = 0.9f;
    public float speed = 0.25f;
    public float transitionRate = 0.5f;

    SpriteRenderer sr;
    SpriteRenderer background;
    BuoyancyEffector2D water;
    BoxCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, transform.position.z);
        sr = GetComponent<SpriteRenderer>();
        Debug.Log(transform.parent.gameObject);
        background = transform.parent.gameObject.GetComponent<SpriteRenderer>();
        water = GetComponent<BuoyancyEffector2D>();
        col = GetComponent<BoxCollider2D>();
        col.size = background.bounds.extents * 2f;
        sr.size = background.bounds.extents * 2f;
        // set initial height
        SetHeight(targetHeight);
    }

    // Update is called once per frame
    void Update()
    {
        if (height != targetHeight)
        {
            height = Mathf.MoveTowards(height, targetHeight, speed * Time.deltaTime);
            SetHeight(height);
        }
    }

    public void SetHeight(float y)
    {
        float yMin = background.bounds.center.y - background.bounds.extents.y;
        float yMax = background.bounds.center.y + background.bounds.extents.y;
        height = y;
        y = Mathf.Lerp(yMin, yMax, y);
        float yPos = (y + yMin) * 0.5f;
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
        float ySize = Mathf.Abs(yMin - y);
        col.size = new Vector2(col.size.x, ySize);
        sr.size = new Vector2(sr.size.x, ySize);
        water.surfaceLevel = 0.5f * ySize;
    }
}