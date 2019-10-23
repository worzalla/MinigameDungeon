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

    bool finish;
    bool ready = true;
    bool minigameSuccess = false;

    // minigames have a time cap
    Minigame minigameState;
    float timer = 0f;
    public float maxTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        minigameState = minigame.GetComponentInChildren<Minigame>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((finish || timer > maxTime) && ready)
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
        finish = true;
        Minigame m = minigame.GetComponentInChildren<Minigame>();
        if (m == null)
        {
            throw new MissingComponentException("GameObject" + minigame + " has no child with a Minigame component.");
        }
        minigameSuccess = m.success;
    }
    IEnumerator TransitionMinigame()
    {
        // disable previous minigame
        minigame.GetComponentInChildren<Minigame>().Disable();
        timer = 0f;
        // display a notification that the player succeeded/failed
        string message = "Failure!";
        if (minigameSuccess)
        {
            message = "Success!";
        }
        Instantiate(notificationPrefab).GetComponentInChildren<UINotification>().Initialize(message);
        yield return new WaitForSeconds(1f);
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
        Instantiate(notificationPrefab).GetComponentInChildren<UINotification>().Initialize(minigameState.title);
        DestroyPreviousMinigame();
        // activate next minigame
        yield return new WaitForSeconds(1f);
        minigameState.Enable();
    }

    // create the game objects for the next minigame, and pass them to the transition object
    private void CreateNextMinigame()
    {
        SpriteRenderer prevMinigameBg = GetMinigameBackground(minigame);
        // create tunnel
        tunnel = Instantiate(tunnelPrefab).GetComponent<Tunnel>();
        Vector3 tunnelPos = tunnel.SetPosition(prevMinigameBg);
        tunnel.SetTargetAlpha(1f);
        // create next minigame (align left bottom corner with right bottom corner of tunnel)
        prevMinigame = minigame;
        minigame = Instantiate(minigamePrefabs[index]);
        SpriteRenderer minigameBg = GetMinigameBackground(minigame);
        Vector3 bgPos = minigame.transform.position - minigameBg.transform.position;
        Vector3 pos = tunnelPos + minigameBg.bounds.extents + bgPos;
        minigame.transform.position = new Vector3(pos.x, pos.y, minigame.transform.position.z);
        // kick off transition object
        transition = Instantiate(transitionPrefab);
        transition.GetComponent<MinigameTransition>().Initialize(prevMinigameBg, tunnel.gameObject, minigameBg);
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
}
