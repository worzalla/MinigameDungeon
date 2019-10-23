using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Controls the notification that zooms onscreen, then leaves.
 */
public class UINotification : MonoBehaviour
{
    public string message;
    public float inTime;
    public float stayTime;
    public float outTime;

    Image image;
    Text text;
    RectTransform canvas;

    // notification x position goes from -canvas.rect.width to +canvas.rect.width

    enum State
    {
        IN, STAY, OUT
    }
    State state = State.IN;

    float timer;

    // must be called to set text
    public void Initialize(string message)
    {
        this.message = message;
    }

    // Start is called before the first frame update
    void Start()
    {
        canvas = transform.parent.GetComponent<RectTransform>();
        image = transform.Find("Image").GetComponent<Image>();
        text = transform.Find("Text").GetComponent<Text>();
        // notification starts fully transparent
        image.color = Color.white;
        text.text = message;
        text.color = Color.white;
        SetX(-getWidth() * 0.5f);
        // start notification in/out animation
        StartCoroutine(Animation());
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        switch (state)
        {
            case State.IN:
                SetX(Mathf.Lerp(-getWidth() * 0.5f, 0, timer / inTime));
                break;
            case State.STAY:
                break;
            case State.OUT:
                SetX(Mathf.Lerp(0, getWidth() * 0.5f, timer / inTime));
                break;
        }
    }

    // get width of canvas + image
    private float getWidth()
    {
        return canvas.rect.width + image.rectTransform.rect.width;
    }

    private void SetX(float x)
    {
        transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
    }

    IEnumerator Animation()
    {
        yield return new WaitForSeconds(inTime);
        timer = 0f;
        state = State.STAY;
        SetX(0);
        yield return new WaitForSeconds(stayTime);
        timer = 0f;
        state = State.OUT;
        yield return new WaitForSeconds(outTime);
        Destroy(transform.parent.parent.gameObject);
    }
}
