using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
 * Randomly chooses minigames, and creates minigame prefabs when necessary.
 * When a minigame completes, spawns a transition and creates the next minigame.
 * Minigames, when finished, report to this object via FinishMinigame()
 * 
 * MinigameController also has a timer, since each Minigame must complete in 5 seconds.
 */
public class MinigameController : MonoBehaviour
{
    // values specified by the Unity Editor
    public GameObject tunnelPrefab;
    public GameObject transitionPrefab;
    public GameObject notificationPrefab;
    public List<GameObject> minigamePrefabs;

    List<GameObject> minigamesRandomlyOrdered;
    int index = 0;
    GameObject prevMinigame;
    public GameObject minigame;
    GameObject transition;
    Tunnel tunnel;

    UIController uiController;

    bool finish;
    bool ready = true;
    bool minigameSuccess = false;

    // minigames have a time cap
    Minigame minigameState;
    float timer = 0f;
    public float maxTime = 5f;

    AudioSource audioSource;
    public AudioClip successSound;
    public AudioClip failSound;

    // Start is called before the first frame update
    void Start()
    {
        uiController = UIController.GetInstance();
        audioSource = GetComponent<AudioSource>();
        if (minigame != null)
        {
            minigameState = minigame.GetComponentInChildren<Minigame>();
            uiController.SetGesture(minigameState.gestureType.ToString());
            minigameState.Enable();
        }
        // if not provided with a starting minigame, spawn one
        else
        {
            finish = true;
        }
    }

    public static MinigameController GetInstance()
    {
        return GameObject.FindGameObjectWithTag("MinigameController").GetComponent<MinigameController>();
    }

    // Update is called once per frame
    void Update()
    {
        // minigame automatically ends at time-up
        if (timer > maxTime)
        {
            FinishMinigame();
        }
        // handles when a minigame has finished
        if (finish && ready)
        {
            ready = false;
            finish = false;
            // play all minigames before repeating one
            // TODO: allow for minigames with different rotation
            index++;
            if (index >= minigamePrefabs.Count)
            {
                ShuffleMinigames();
                index = 0;
            }
            // start transition
            StartCoroutine(TransitionMinigame());
        }
        if (minigameState != null && minigameState.GetActive())
        {
            timer += Time.deltaTime;
        }
    }

    // called by Minigame component to signal that a minigame has finished
    // triggers the transition to the next minigame
    public void FinishMinigame()
    {
        if (ready)
        {
            finish = true;
            Minigame m = minigame.GetComponentInChildren<Minigame>();
            if (m == null)
            {
                throw new MissingComponentException("GameObject" + minigame + " has no child with a Minigame component.");
            }
            minigameSuccess = m.success;
            m.Disable();
        }
    }
    IEnumerator TransitionMinigame()
    {
        timer = 0f;
        // display a notification that the player succeeded/failed
        PlayerInfo info = uiController.GetComponent<PlayerInfo>();
        if (minigame)
        {
            string message = minigameSuccess ? "Success!" : "Failure!";
            info.level++;
            if (minigameSuccess)
            {
                audioSource.PlayOneShot(successSound);
                info.score++;
            }
            else
            {
                audioSource.PlayOneShot(failSound);
                info.health = Math.Max(0, info.health - 1);
                GameObject.FindGameObjectWithTag("HeartArray").GetComponent<UIHeartArray>().SetHeartDisplay(info.health);
            }
            Instantiate(notificationPrefab).GetComponentInChildren<UINotification>().Initialize(message);
            yield return new WaitForSeconds(1f);
        }
        // if player is dead, do not create next minigame
        // sit here and pause forever on a disabled minigame
        if (info.health <= 0)
        {
            uiController.EndGame();
            yield break;
        }
        // create next minigame
        CreateNextMinigame();
        while (transition != null)
        {
            yield return null;
        }
        // create 
        minigameState = minigame.GetComponentInChildren<Minigame>();
        if (minigameState == null)
        {
            throw new MissingComponentException("GameObject" + minigame + " has no child with a Minigame component.");
        }
        uiController.SetGesture(minigame.GetComponentInChildren<Minigame>().gestureType.ToString());
        Instantiate(notificationPrefab).GetComponentInChildren<UINotification>().Initialize(minigameState.title);
        DestroyPreviousMinigame();
        // activate next minigame
        yield return new WaitForSeconds(1f);
        minigameState.Enable();
    }

    // create the game objects for the next minigame, and pass them to the transition object
    private void CreateNextMinigame()
    {
        SpriteRenderer prevMinigameBg = null;
        SpriteRenderer tunnelPos = null;
        if (minigame != null)
        {
            prevMinigameBg = GetMinigameBackground(minigame);
            // create tunnel
            tunnel = Instantiate(tunnelPrefab).GetComponent<Tunnel>();
            tunnelPos = tunnel.SetPosition(prevMinigameBg);
            tunnel.SetTargetAlpha(1f);
            // create next minigame (align player positions)
            prevMinigame = minigame;
        }
        minigame = Instantiate(minigamePrefabs[index]);
        if (tunnel == null)
        {
            minigame.transform.position = Vector3.zero;
            return;
        }
        SpriteRenderer minigameBg = GetMinigameBackground(minigame);
        PlayerStartingPosition minigamePlayer = minigame.GetComponentInChildren<PlayerStartingPosition>();
        if (minigamePlayer == null)
        {
            throw new MissingComponentException("GameObject" + minigame + " has no child with a PlayerStartingPosition component.");
        }
        float playerY = minigamePlayer.transform.position.y - minigameBg.transform.position.y;
        float bgPosY = Mathf.Clamp(tunnel.transform.position.y - playerY,
            tunnel.transform.position.y + tunnelPos.bounds.extents.y - minigameBg.bounds.extents.y,
            tunnel.transform.position.y - tunnelPos.bounds.extents.y + minigameBg.bounds.extents.y);
        Vector2 pos = new Vector2(tunnel.transform.position.x + tunnelPos.bounds.extents.x + minigameBg.bounds.extents.x + tunnel.GetOffset().x, bgPosY);
        // convert bg to minigame coords
        pos += (Vector2)(minigame.transform.position - minigameBg.transform.position);
        minigame.transform.position = new Vector3(pos.x, pos.y, minigame.transform.position.z);

        // kick off transition object
        transition = Instantiate(transitionPrefab);
        transition.GetComponent<MinigameTransition>().Initialize(prevMinigameBg, tunnel.gameObject, minigameBg, 
           minigame.GetComponentInChildren<PlayerStartingPosition>());
    }
    // run once the transition object completes
    private void DestroyPreviousMinigame()
    {
        Destroy(prevMinigame);
        StartCoroutine(DestroyTunnel());
    }

    private SpriteRenderer GetMinigameBackground(GameObject minigame)
    {
        Transform[] children = minigame.GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            // look for minigame background
            if (child.tag == "MinigameBackground")
            {
                SpriteRenderer bg = child.GetComponent<SpriteRenderer>();
                if (bg == null)
                {
                    throw new ArgumentException("Minigame GameObject " + gameObject + "'s MinigameBackground child has no SpriteRenderer.");
                }
                return bg;
            }
        }
        throw new ArgumentException("Minigame GameObject " + gameObject + " has no child with a MinigameBackground tag.");
    }

    // shuffle minigame order
    // TODO: allow for minigames with different rotation
    private void ShuffleMinigames()
    {
        System.Random r = new System.Random();
        minigamesRandomlyOrdered = Enumerable.Range(0, minigamePrefabs.Count).OrderBy(x => r.Next()).Select(i => minigamePrefabs[i]).ToList();
    }

    // destroy the tunnel graphic at end of transition
    IEnumerator DestroyTunnel()
    {
        if (tunnel != null)
        {
            tunnel.SetTargetAlpha(0f);
        }
        while (tunnel != null && tunnel.alpha != 0f)
        {
            yield return null;
        }
        if (tunnel != null)
        {
            Destroy(tunnel.gameObject);
        }
        ready = true;
    }

    public float GetTimer()
    {
        return 1f - (timer / maxTime);
    }

    public void Delete()
    {
        Player player = Player.GetInstance();
        Destroy(player.gameObject);
        Destroy(minigame);
        Destroy(gameObject);
        Camera.main.transform.position = Vector3.back * 20f;
    }
}
