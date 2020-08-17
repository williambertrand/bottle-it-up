using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HumanStateManagement
{
    //TODO: Should probably define a MovingState that we inherit from for moving to item, to checkout, or to exit
    public class MoveToItemHumanState : HumanState
    {

        private bool hasReachedDestination;

        public MoveToItemHumanState(Human human, HumanStateHandler stateMachine) : base(human, stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            //item = human.NextItem
            //Set destination position based on next item on "shopping" list
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            //TODO: Check if human has reached its destination
            if (!human.agent.pathPending && human.agent.remainingDistance < 0.6f)
                stateMachine.ChangeState(human.collectItem);
        }

    }
}
