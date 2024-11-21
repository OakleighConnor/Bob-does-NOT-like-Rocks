using UnityEngine;
namespace Rock
{
    public class WeakState : State
    {
        // constructor
        public WeakState(RockScript rock, StateMachine sm) : base(rock, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Weak state entered");
        }

        public override void Exit()
        {
            base.Exit();
            Debug.Log("Weak state exitted");
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();


            // If the player is on an unstable tile (hard tile, rock)
            if (rock.hardTile.GetTile(rock.hardTile.WorldToCell(rock.groundCheck.position)) || Physics2D.OverlapCircle(rock.groundCheck.position, .2f, rock.otherRock))
            {
                Debug.Log("On hard tile");

                Vector3 right;
                right = rock.transform.position;
                right.x += 1;

                if (!rock.CheckForTile(right) && right != rock.playerMovePoint.position)
                {
                    // Checks the bottom right tile
                    right.y -= 1;
                    if (!rock.CheckForTile(right) && right != rock.playerMovePoint.position)
                    {
                        rock.groundCheck.position += new Vector3(1f, 1f, 0f);
                        rock.sm.ChangeState(rock.fallState);
                    }
                }

                Vector3 left;
                left = rock.transform.position;
                left.x -= 1;

                if (!rock.CheckForTile(left) && left != rock.playerMovePoint.position)
                {
                    // Checks the bottom right tile
                    left.y -= 1;
                    if (!rock.CheckForTile(left) && left != rock.playerMovePoint.position)
                    {
                        rock.groundCheck.position += new Vector3(-1f, 1f, 0f);
                        rock.sm.ChangeState(rock.fallState);
                    }
                }
            }



            rock.CheckForFall();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
