using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
public class PlayerScript : MonoBehaviour
{
    public float speed = 5f;
    public Vector2 moveDir;
    public float audioSpeed;
    public bool stop = false;

    [Header("References")]
    public Animator anim;
    SpriteRenderer sr;
    public AudioManager am;
    public DoorManager dm;
    ResultsButtons ui;

    [Header("Transforms")]
    public Transform movePoint;
    public Transform door;

    [Header("Tilemaps")]
    public Tilemap map;

    [Header("LayerMasks")]
    public LayerMask obstruction;
    public LayerMask doorLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (stop) return;

        // Moves player towards the move point
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);

        KeyboardMovement();
        Animation();

        SwipeDetection.instance.swipePerformed += context => { Movement(context); };

    }
    public void LoadPlayer()
    {
        audioSpeed = am.startingPitch;
        am.ChangeMusic(am.level);

        movePoint.parent = null;
        door = GameObject.FindGameObjectWithTag("Door").transform;
        dm = door.GetComponent<DoorManager>();
        anim = gameObject.GetComponentInChildren<Animator>();
        sr = gameObject.GetComponentInChildren<SpriteRenderer>();
        ui = FindAnyObjectByType<ResultsButtons>();

        am.PlaySFX(am.loading);
    }

    public void Movement(Vector2 direction)
    {
        if (Vector3.Distance(transform.position, movePoint.position) != 0f) return;

        if(audioSpeed < 2f)
        {
            audioSpeed += audioSpeed / 5f;
        }

        if (direction.x <= 0)
        {
            moveDir.x = direction.x * -1;
        }
        else
        {
            moveDir.x = direction.x;
        }

        if (direction.y <= 0)
        {
            moveDir.y = direction.y * -1;
        }
        else
        {
            moveDir.y = direction.y;
        }

        // Compare input of x and y to determine which direction the player input most to move in.

        if(moveDir.x >= moveDir.y)
        {
            // Player moves left/right

            direction.x = Mathf.Clamp(direction.x, -1, 1);

            // Checks for collisions in the direction you're moving
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(direction.x, 0f, 0f), .2f, obstruction))
            {
                movePoint.position += new Vector3(direction.x, 0f, 0f);
            }
        }
        else
        {
            // Player moves up/down

            direction.y = Mathf.Clamp(direction.y, -1, 1);

            // Checks for collisions in the direction you're moving
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, direction.y, 0f), .2f, obstruction))
            {
                movePoint.position += new Vector3(0f, direction.y, 0f);
            }
        }

        if (map.GetTile(map.WorldToCell(movePoint.position)))
        {
            map.SetTile(map.WorldToCell(movePoint.position), null);
            am.PlaySFX(am.dig);
        }

        if (transform.position.x > movePoint.position.x)
        {
            // left
            sr.flipX = true;
        }
        else
        {
            // right
            sr.flipX = false;
        }
    }

    public void KeyboardMovement()
    {
        if (map.GetTile(map.WorldToCell(movePoint.position)))
        {
            map.SetTile(map.WorldToCell(movePoint.position), null);
            am.PlaySFX(am.dig);
        }


        // Controls movement to make sure player only moves 1 grid at a time
        if (Vector3.Distance(transform.position, movePoint.position) == 0f)
        {

            audioSpeed = am.SetMusicSpeed(audioSpeed);


            if (Input.GetAxisRaw("Horizontal") == 1f || Input.GetAxisRaw("Vertical") == 1f)
            {
                if (audioSpeed < 2f)
                {
                    audioSpeed += audioSpeed / 5f;
                }
            }

            // Break the tile that the player walks onto
            if (map.GetTile(map.WorldToCell(movePoint.position)))
            {
                map.SetTile(map.WorldToCell(movePoint.position), null);
            }

            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                // Checks for collisions in the direction you're moving
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, obstruction))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                    return; // Prevent the player from being able to move diagonally (does result in the player prioritising left and right movement)
                }
            }

            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, obstruction))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                    return;
                }
            }
        }
    }

    public void Death()
    {
        if (stop) return;
        ui.GameOver();
        Debug.Log("Player Death");
        sr.enabled = false;
        am.PlaySFX(am.dig);
        am.StopMusic();
        stop = true;
    }

    public void Animation()
    {
        if (Vector3.Distance(transform.position, movePoint.position) == 0f)
        {
            anim.SetBool("Moving", false);
        }
        else
        {
            anim.SetBool("Moving", true);
        }
    }
}
