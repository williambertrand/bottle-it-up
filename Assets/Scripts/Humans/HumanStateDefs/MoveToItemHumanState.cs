using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HumanStateManagement
{
    //TODO: Should probably define a MovingState that we inherit from for moving to item, to checkout, or to exit
    public class MoveToItemHumanState : MoveHumanState
    {

        public MoveToItemHumanState(
            Human human,
            HumanStateHandler stateMachine,
            Animator animator) : base(human, stateMachine, animator)
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

        public override void Pause()
        {
            base.Pause();
        }

        public override void Resume()
        {
            base.Resume();
        }

        public override void Update()
        {
            base.Update();

            //TODO: Check if human has reached its destination
            if (!human.agent.pathPending && human.agent.remainingDistance < 0.75f)
            {
                Debug.Log("MOVE TO COLLECT!!!");
                stateMachine.ChangeState(human.collectItem);
            }
        }

        public override string ToString()
        {
            return "MoveToItem";
        }

    }
}
