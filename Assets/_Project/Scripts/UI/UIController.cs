using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private static UIController instance;
    private MinigameController minigameController;
    public SimpleHealthBar healthBar;

    public GameObject[] HeartArray;
    public Text LevelText;
    public Overlay Overlay;

    public string defaultScreen = "";

    private string m_userEmail = "";
    private bool m_menuOn = false;
    
   /** Sets up screen control in m_screens. 
    *  IMPORTANT: Overlays should be independent (only one gob in m_screens is shown, so it would be overlaying nothing)
    */
    private void Awake()
    {
        instance = this;
        minigameController = MinigameController.GetInstance();
        ToggleOverlay();
        SetCurrentScreen("Menu");
    }

    private void Update()
    {
        healthBar.UpdateBar(minigameController.maxTime * minigameController.GetTimer(), minigameController.maxTime);
    }

    void StartGame ()
    {
        // Create minigame controller
        print("Start game");
    }

    // Snaps away the minigame controller and lets user sign up for gdd and stuff if they want
    void EndGame ()
    {
        // Destroy minigame controller
        print("Avengers: Endgame");
    }

    public void SignUpForGDD ()
    {
        bool success = minigameController.GetComponent<MinigameDungeonWebRequest>().SendEmail(m_userEmail);
        string title = success ? "Success!" : "Failure";
        string text = success ? "You have signed up for GDD!" :
            "Sorry, something went wrong. Please try again.";
        Overlay.SetScreen(title, text, false);
    }
    public void SetEmail (string email)
    {
        m_userEmail = email;
    }
    public void ToggleOverlay()
    {
        m_menuOn = !m_menuOn;
        Overlay.SetActive(m_menuOn);
    }

    // Remove 1 heart from array, if no hearts left ends game
    public static void TakeYourHeart()
    {
        bool outOfHearts = false;
        Debug.Log("Taking a heart");
        // instance.HeartArray
        if (outOfHearts) instance.EndGame();
    }
    
    public static void ShowControls (string type)
    {
        // if type = x then show indicator X
    }

    public void SetCurrentScreen(string screen)
    {
        if (screen == "Menu")
        {
            Overlay.SetScreen("Main Menu", "", false);
        }
        else if(screen == "Play")
        {
            ToggleOverlay();
            StartGame();
        }
        else if (screen == "Sign Up")
        {
            Overlay.SetScreen("Sign up for GDD", "Do it u nerd", true);
        }
        else if (screen == "Info")
        {
            Overlay.SetScreen("Information", "Info placeholder", false);
        }
    }
}

