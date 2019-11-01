using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overlay : MonoBehaviour
{
    public Button CloseButton;
    public Text Title;
    public Text Text;
    public GameObject EmailSubmissionForm;
    public GameObject Navigation;

    public void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }
    public void SetScreen(string title, string text, bool isEmail)
    {
        this.Title.text = title;
        this.Text.text = text;
        this.EmailSubmissionForm.SetActive(isEmail);
        this.Navigation.SetActive(!isEmail);
    }
}
