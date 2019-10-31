using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Links touch spin input to lowering the water level
 */
public class SpinValveSpinner : MonoBehaviour
{
    float prevAngle = 0;
    float value = 0;
    // rotations until goal
    public float waterLevelStart = 0.85f;
    public float rotations = 10f;

    SpinValveWater water;

    // Start is called before the first frame update
    void Start()
    {
        water = transform.parent.gameObject.GetComponentInChildren<SpinValveWater>();
        value = (1f - waterLevelStart) * (rotations * 360f);
        transform.eulerAngles = new Vector3(0f, 0f, value);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Minigame.isActive)
        {
            return;
        }
        // mouse spin input
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            prevAngle = DirectionTo(pos, 0f);
        }
        if (Input.GetMouseButton(0))
        {
            float dir = DirectionTo(pos, prevAngle);
            value += dir - prevAngle;
            prevAngle = dir;
        }
        transform.eulerAngles = new Vector3(0f, 0f, value);
        // based on angle, lower water rate.
        // success when water level is at 0
        float waterLevel = 1f - Mathf.InverseLerp(0f, rotations * 360f, value);
        water.targetHeight = waterLevel;
        if (water.height < 0.1f)
        {
            Minigame.SetSuccess(true);
        }
        if (water.height < 0.01f)
        {
            Minigame.FinishMinigame();
        }
    }

    float DirectionTo(Vector2 v, float angle)
    {
        Vector2 dir = v - (Vector2)transform.position;
        float a = Vector2.SignedAngle(Vector2.right, dir.normalized);
        return angle + Mathf.DeltaAngle(angle, a);
    }
}
