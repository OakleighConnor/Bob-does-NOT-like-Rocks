using UnityEngine.Tilemaps;
using UnityEngine;
using UnityEditor.ShaderGraph;

namespace Rock
{


    public class RockScript : MonoBehaviour
    {
        [Header("LayerMasks")]
        public LayerMask otherRock;

        [Header("Checks")]
        public Transform groundCheck;
        public Transform playerMovePoint;

        // Tilemaps
        [Header("Tilemaps")]
        public Tilemap hardTile;
        public Tilemap dirtTile;

        // variables holding the different player states
        [Header("States")]
        public IdleState idleState;
        public WeakState weakState;
        public FallingState fallState;

        [Header("References")]
        public StateMachine sm;
        public PlayerScript player;
        public AudioManager am;


        void Awake()
        {
            groundCheck.parent = null;
        }
        // Start is called before the first frame update
        void Start()
        { 
            sm = gameObject.AddComponent<StateMachine>();
            player = FindAnyObjectByType<PlayerScript>();

            // add new states here
            idleState = new IdleState(this, sm);
            weakState = new WeakState(this, sm);
            fallState = new FallingState(this, sm);

            playerMovePoint = player.movePoint;

            // initialise the statemachine with the default state
            sm.Init(idleState);
        }

        // Update is called once per frame
        public void Update()
        {
            sm.CurrentState.LogicUpdate();

            if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log(sm.CurrentState.ToString());
            }
        }



        void FixedUpdate()
        {
            sm.CurrentState.PhysicsUpdate();
        }

        public void CheckForWeakness()
        {
            // TODO: Check if another rock is beneath this rock.

            // Set to weak state

            if(groundCheck.position == playerMovePoint.position || hardTile.GetTile(hardTile.WorldToCell(groundCheck.position)) || Physics2D.OverlapCircle(groundCheck.position, .2f, otherRock))
            {
                sm.ChangeState(weakState);
            }
        }

        public void CheckForFall()
        {
            // TODO: While weak, check if there is a gap beneath or next to the rock. If so then fall in the gap

            
            if (hardTile.GetTile(hardTile.WorldToCell(groundCheck.position)) || Physics2D.OverlapCircle(groundCheck.position, .2f, otherRock))
            {
                // If the rock is on a hard tile

                // Check if there is space on the left, right, and bottom diagonals for the rock to fall;

                return;

            }
            else if (groundCheck.position != playerMovePoint.position)
            {
                sm.ChangeState(fallState);
            }
        }

        public bool CheckForTile(Vector3 checkPos) // Returns true if a tile is in the way, else it returns false
        {
            if (hardTile.GetTile(hardTile.WorldToCell(checkPos)))
            {
                Debug.Log("Hard");
            }
            if (dirtTile.GetTile(dirtTile.WorldToCell(checkPos)))
            {
                Debug.Log("Dirt");
            }


            if (hardTile.GetTile(hardTile.WorldToCell(checkPos)) || dirtTile.GetTile(dirtTile.WorldToCell(checkPos)) || Physics2D.OverlapCircle(checkPos, .2f, otherRock))
            {
                Debug.Log("Tile");

                // Return true if a tile is occupying the position
                return true;
            }
            return false;
        }
    }

}