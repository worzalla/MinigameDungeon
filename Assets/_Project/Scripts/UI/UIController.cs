using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private MinigameController minigameController;
    public SimpleHealthBar healthBar;

    public GameObject[] HeartArray;
    public Text LevelText;
    public Overlay Overlay;
    public GameObject HUD;

    public GameObject heartArrayPrefab;
    public GameObject hearts;

    [System.Serializable]
    public class GestureDictionary
    {
        [System.Serializable]
        public struct Entry
        {
            public string name;
            public Sprite img;    
            public string GetName() { return name;}
            public Sprite GetImage() { return img;}
        }
        
        public Entry[] entries;
        public Sprite GetImageByName(string name)
        {
            if (name == "" || name == null) name = "Default";
            foreach (Entry e in entries)
            {
                if (e.GetName() == name) return e.GetImage();
            }
            return null;
        }
    }

    public GestureDictionary gestures;
    public Image TouchImg;
    public Text TouchName;

    private PlayerInfo info;

    public GameObject minigameControllerInstance;

    public string defaultScreen = "";

    private string m_userEmail = "";
    private bool m_menuOn = false;
    
    public static UIController GetInstance()
    {
        return GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
    }

   /** Sets up screen control in m_screens. 
    *  IMPORTANT: Overlays should be independent (only one gob in m_screens is shown, so it would be overlaying nothing)
    */
    private void Awake()
    {
        info = UIController.GetInstance().GetComponent<PlayerInfo>();
        ToggleOverlay();
        SetCurrentScreen("Menu");
    }

    private void Update()
    {
        if (minigameController != null)
        {
            healthBar.UpdateBar(minigameController.maxTime * minigameController.GetTimer(), minigameController.maxTime);
        }
        LevelText.text = "Level " + info.score;
    }

    void StartGame ()
    {
        // Create minigame controller
        info.Reset();
        minigameController = Instantiate(minigameControllerInstance).GetComponent<MinigameController>();
    }

    // Snaps away the minigame controller and lets user sign up for gdd and stuff if they want
    public void EndGame ()
    {
        minigameController.Delete();
        SetCurrentScreen("Game Over");
        ToggleOverlay();
    }

    public void SignUpForGDD ()
    {
        StartCoroutine(SignUpCoroutine());
    }
    IEnumerator SignUpCoroutine()
    {
        MinigameDungeonWebRequest mdwr = GetComponent<MinigameDungeonWebRequest>();
        if (!mdwr.ready)
        {
            // if a request is still on the way, ignore repeated button press.
            yield break;
        }
        mdwr.SendEmail(m_userEmail);
        // disable form while request is on the way
        string loading = "Signing up...";
        Overlay.SetScreen(loading, "This may take a minute.", false);
        // wait until web request is complete
        int c = 0;
        while (!mdwr.complete)
        {
            c++;
            if (c % 60 == 0 && loading.Length < 25)
            {
                loading = loading + ".";
                Overlay.SetScreen(loading, "This may take a minute.", false);
            }
            yield return null;
        }
        // when web request is complete, fetch result
        long result = mdwr.result;
        mdwr.ready = true;
        string title = result == 200 ? "Success!" : "Failure";
        string text = "You have signed up for GDD!";
        switch (result)
        {
            case 200:
                break;
            case 400:
                text = "Sorry, only UW emails ending in '@wisc.edu' can be used to sign up.";
                break;
            case 404:
                text = "Please enter an email.";
                break;
            case 406:
                text = "The UW email entered does not exist.";
                break;
            case 422:
                title = "Success?";
                text = "The UW email entered has already been signed up.";
                break;
            default:
                text = "Sorry, something went wrong. Please try again.";
                break;
        }
        Overlay.SetScreen(title, text, false);
        yield break;
    }

    public void SetEmail (string email)
    {
        m_userEmail = email;
    }
    public void ToggleOverlay()
    {
        m_menuOn = !m_menuOn;
        Overlay.SetActive(m_menuOn);
        HUD.SetActive(!m_menuOn);
        if (!m_menuOn)
        {
            // reset heart array
            if (hearts != null)
            {
                Destroy(hearts);
            }
            hearts = Instantiate(heartArrayPrefab, HUD.transform);
        }
    }
    
    public static void ShowControls (string type)
    {
        // if type = x then show indicator X
    }

    public void SetCurrentScreen(string screen)
    {
        if (screen == "Menu")
        {
            Overlay.SetScreen("Minigame Dungeon", "", false);
        }
        else if(screen == "Play")
        {
            ToggleOverlay();
            StartGame();
        }
        else if (screen == "Sign Up")
        {
            Overlay.SetScreen("Sign up for GDD", "Game Design and Development is a register student org at UW-Madison that focuses on the creation of games. Please leave your email if you would like more information.", true);
        }
        else if (screen == "Info")
        {
            Overlay.SetScreen("How to Play", "Minigame Dungeon is a series of minigames. You have 5 seconds to complete each minigame. Look at the bottom left corner for hints on how to play the minigame. You have 3 lives. Try to survive as long as possible!\n\nMinigame Dungeon was made by Evans Chen, Emma Tracy, Jun Yu \"Jimmy\" Ma, Vinoth Manoharan, and Skylyn Worzalla for CS 506 at UW-Madison", false);
        }
        else if (screen == "Game Over")
        {
            Overlay.SetScreen("Game Over", "You're out of hearts! Play again or sign up for GDD!", false);
        }
    }

    /** 
     *  Accepts: "TILT", "DRAG", "TAP", "TILT_AND_TAP" and "SWIPE"
     */
    public void SetGesture(string gesture)
    {
        TouchName.text = gesture;
        if (gesture == "TILT_AND_TAP")
        {
            TouchName.text = "TILT+TAP";
        }
        TouchImg.sprite = gestures.GetImageByName(gesture);
    }
}

