using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    GameObject[] keyArray;
    public List<GameObject> keys = new List<GameObject>();

    public bool doorOpen;

    PlayerScript player;
    Animator anim;
    ResultsButtons ui;
    public AudioManager am;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadDoor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadDoor()
    {
        player = FindAnyObjectByType<PlayerScript>();

        doorOpen = false;

        ui = FindAnyObjectByType<ResultsButtons>();

        anim = GetComponent<Animator>();

        keyArray = GameObject.FindGameObjectsWithTag("Key");
        
        for (int i = 0; i < keyArray.Length; i++)
        {
            keys.Add(keyArray[i]);
        }
    }

    public void KeyCollected(GameObject key)
    {
        am.PlaySFX(am.key);

        for (int i = 0; i < keys.Count; i++)
        {
            if(keys[i].gameObject == key)
            {
                Destroy(key);
                keys.RemoveAt(i);
                Debug.Log("Key Collected");
            }
        }

        if(keys.Count == 0)
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        anim.Play("Opened");
        doorOpen = true;
        am.PlaySFX(am.doorOpen);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player") && doorOpen)
        {
            player.anim.Play("Exit");
            player.stop = true;
            am.StopMusic();
            am.PlaySFX(am.playerEscape);
            ui.Win();
        }
    }
}