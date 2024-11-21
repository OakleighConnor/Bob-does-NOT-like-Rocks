using UnityEngine;
namespace Rock
{
    public class FallingState : State
    {
        // constructor
        public FallingState(RockScript rock, StateMachine sm) : base(rock, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Fall state entered");
        }

        public override void Exit()
        {
            base.Exit();
            Debug.Log("Fall state exitted");
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
                rock.groundCheck.position += new Vector3(0f, -1, 0f);

                if (rock.CheckForTile(rock.groundCheck.position))
                {
                    rock.sm.ChangeState(rock.idleState);
                }

            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            rock.transform.position = Vector3.MoveTowards(rock.transform.position, rock.groundCheck.position, 5 * Time.deltaTime);
        }
    }
}
