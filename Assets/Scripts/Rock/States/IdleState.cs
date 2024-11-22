using UnityEngine;
namespace Rock
{
    public class IdleState : State
    {
        // constructor
        public IdleState(RockScript rock, StateMachine sm) : base(rock, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            // Check the tile beneath the rock
            base.LogicUpdate();
            rock.CheckForWeakness();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
