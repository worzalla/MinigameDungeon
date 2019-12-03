using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
                activity.Call<bool>("moveTaskToBack", true);
            }
            else
            {
                Application.Quit();
            }
        }
    }

    public static Rect CameraRect()
    {
        Vector2 min = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector2 max = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        return new Rect(min, max - min);
    }
    public static Rect CameraRect(Vector2 bounds)
    {
        Rect rect = CameraRect();
        rect.xMin -= bounds.x;
        rect.xMax += bounds.x;
        rect.yMin -= bounds.y;
        rect.yMax += bounds.y;
        return rect;
    }
}
