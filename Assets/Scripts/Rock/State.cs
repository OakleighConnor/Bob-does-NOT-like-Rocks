using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Rock
{
    public abstract class State
    {
        protected RockScript rock;
        protected StateMachine sm;


        // base constructor
        protected State(RockScript rock, StateMachine sm)
        {
            this.rock = rock;
            this.sm = sm;
        }

        // These methods are common to all states
        public virtual void Enter()
        {
            //Debug.Log("This is base.enter");
        }

        public virtual void HandleInput()
        {
        }

        public virtual void LogicUpdate()
        {
        }

        public virtual void PhysicsUpdate()
        {
        }

        public virtual void Exit()
        {
        }

    }

}