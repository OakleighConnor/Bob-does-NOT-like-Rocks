using UnityEngine;
namespace Rock
{
    public class FallingState : State
    {
        Vector3 lastPos;

        // constructor
        public FallingState(RockScript rock, StateMachine sm) : base(rock, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            rock.am.PlaySFX(rock.am.rock);
            base.Exit();
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if(Vector3.Distance(rock.transform.position, rock.groundCheck.position) == 0f)
            {
                lastPos = rock.groundCheck.position;

                rock.groundCheck.position += new Vector3(0f, -1, 0f);
                
                if (rock.CheckForTile(rock.groundCheck.position))
                {
                    rock.sm.ChangeState(rock.idleState);
                }

            }

            if(rock.player.transform.position == rock.groundCheck.position)
            {
                rock.player.Death();
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            rock.transform.position = Vector3.MoveTowards(rock.transform.position, rock.groundCheck.position, 5 * Time.deltaTime);
        }
    }
}
