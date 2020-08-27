using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HumanStateManagement
{
    //TODO: Should probably define a MovingState that we inherit from for moving to item, to checkout, or to exit
    public class MoveHumanState : HumanState
    {

        private Vector3 navDest;
        private Animator animator;

        public MoveHumanState(Human human, HumanStateHandler stateMachine, Animator animator) : base(human, stateMachine)
        {
            this.animator = animator;
        }

        public override void Enter()
        {
            base.Enter();
            animator.SetTrigger("Walk");
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Pause()
        {
            base.Pause();

            //Save the navmesh destination for when we resume this state
            navDest = human.agent.destination;

        }

        public override void Resume()
        {
            base.Resume();
            human.agent.destination = navDest;
        }

        public override void Update()
        {
            base.Update();
        }

        public override string ToString()
        {
            return "Move";
        }

    }
}
