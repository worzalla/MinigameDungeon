using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimBallistaMinigame : MonoBehaviour
{
    Vector2 prev;
    bool clicked = false;
    AimBallistaLine line;
    AimBallistaBolt bolt;
    GameObject head;

    // Start is called before the first frame update
    void Start()
    {
        Minigame.SetSuccess(false);
        line = GetComponentInChildren<AimBallistaLine>();
        bolt = GetComponentInChildren<AimBallistaBolt>();
        head = transform.Find("Ballista").Find("Head").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Minigame.isActive)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            clicked = true;
            prev = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 diff = pos - prev;
        if (clicked && diff.magnitude > 0.1f)
        {
            line.SetAlpha(1);
            float angle = Vector2.SignedAngle(Vector2.right, diff.normalized);
            angle = ClampAngle(angle, 0f, 90f);
            float revAngle = Vector2.SignedAngle(Vector2.right, -diff.normalized);
            revAngle = ClampAngle(revAngle, 0f, 90f);
            if ((angle == 0f || angle == 90f) && (revAngle != 0f && revAngle != 90f))
            {
                angle = revAngle;
            }
            Vector2 dir = (Quaternion.Euler(0, 0, angle) * Vector2.right);
            line.SetPos(head.transform.position, (Vector2)head.transform.position + (dir * 16f));
            head.transform.eulerAngles = new Vector3(0f, 0f, angle);
            if (Input.GetMouseButtonUp(0))
            {
                line.SetAlpha(0);
                clicked = false;
                bolt.Launch(dir * 20f, angle);
            }
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        angle = Mathf.Repeat(angle, 360);
        min = Mathf.Repeat(min, 360);
        max = Mathf.Repeat(max, 360);
        bool inverse = false;
        var tmin = min;
        var tangle = angle;
        if (min > 180)
        {
            inverse = !inverse;
            tmin -= 180;
        }
        if (angle > 180)
        {
            inverse = !inverse;
            tangle -= 180;
        }
        var result = !inverse ? tangle > tmin : tangle < tmin;
        if (!result)
            angle = min;

        inverse = false;
        tangle = angle;
        var tmax = max;
        if (angle > 180)
        {
            inverse = !inverse;
            tangle -= 180;
        }
        if (max > 180)
        {
            inverse = !inverse;
            tmax -= 180;
        }

        result = !inverse ? tangle < tmax : tangle > tmax;
        if (!result)
            angle = max;
        return angle;
    }
}
