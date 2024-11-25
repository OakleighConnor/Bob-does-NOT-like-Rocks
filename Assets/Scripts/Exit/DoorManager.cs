using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public GameObject[] keyArray;
    public List<GameObject> keys = new List<GameObject>();

    public bool doorOpen;

    public Animator anim;

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
        doorOpen = false;

        anim = GetComponent<Animator>();

        keyArray = GameObject.FindGameObjectsWithTag("Key");
        
        for (int i = 0; i < keyArray.Length; i++)
        {
            keys.Add(keyArray[i]);
        }
    }

    public void KeyCollected(GameObject key)
    {
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
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player") && doorOpen)
        {
            Debug.Log("Exit");
        }
    }
}