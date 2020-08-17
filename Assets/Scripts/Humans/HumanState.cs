/**
 *
 *  Entry: Perform whatever action(s) should only be done once upon entry
 *  Exit: Clean-ups to be done once before the state changes
 *  Update Loop: Executes in every frame 
 *
 */

namespace HumanStateManagement
{
    public abstract class HumanState
    {
        protected HumanStateHandler stateMachine;
        protected Human human;

        protected HumanState(Human human, HumanStateHandler stateMachine)
        {
            this.human = human;
            this.stateMachine = stateMachine;
        }

        public virtual void Enter() { }

        public virtual void Update() { }

        public virtual void FixedUpdate() { }

        public virtual void Exit() { }


    }
}