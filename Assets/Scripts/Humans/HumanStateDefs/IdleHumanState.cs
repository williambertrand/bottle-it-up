using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HumanStateManagement
{
    public class IdleHumanState : HumanState
    {
        protected Animator animator;

        public IdleHumanState(Human human, HumanStateHandler stateMachine, Animator animator) : base(human, stateMachine)
        {
            this.animator = animator;
        }

        public override void Enter()
        {
            base.Enter();
            animator.SetTrigger("Idle");
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            //Check if human has another item to shop for, once it does, move on to moving state
        }
    }
}
