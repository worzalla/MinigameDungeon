using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimBallistaLine : MonoBehaviour
{
    LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAlpha(float alpha)
    {
        line.startColor = new Color(line.startColor.r, line.startColor.g, line.startColor.b, alpha);
        line.endColor = new Color(line.endColor.r, line.endColor.g, line.endColor.b, alpha);
    }

    public void SetPos(Vector2 start, Vector2 end)
    {
        line.SetPosition(0, start);
        line.SetPosition(1, end);
    }
}
