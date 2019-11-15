using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MinigameDungeonWebRequest : MonoBehaviour
{
    private bool m_result;

    public bool SendEmail(string email)
    {
        StartCoroutine(Upload(email));
        return m_result;
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
                m_result = false;
            }
            else
            {
                m_result = true;
            }
        }
    }
}