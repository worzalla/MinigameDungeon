using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MinigameDungeonWebRequest : MonoBehaviour
{
    public long result;

    // result ready and result fetched
    public bool ready = true;

    // result ready but not yet fetched
    public bool complete = true;

    void Start()
    {
        ready = true;
        complete = true;
    }

    public void SendEmail(string email)
    {
        ready = false;
        complete = false;
        StartCoroutine(Upload(email));
    }

    IEnumerator Upload(string email)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);

        using (UnityWebRequest www = UnityWebRequest.Post("http://minecraft.scrollingnumbers.com:42069/signup", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                complete = true;
                result = www.responseCode;
            }
            else
            {
                complete = true;
                result = www.responseCode;
            }
        }
    }
}