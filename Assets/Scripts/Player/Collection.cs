using UnityEngine;

public class Collection : MonoBehaviour
{
    DoorManager door;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            door = FindAnyObjectByType<DoorManager>();

            door.KeyCollected(gameObject);
        }
    }
}
