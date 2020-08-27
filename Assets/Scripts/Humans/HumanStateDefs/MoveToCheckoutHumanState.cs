using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HumanStateManagement
{
    //TODO: Should probably define a MovingState that we inherit from for moving to item, to checkout, or to exit
    public class MoveToCheckoutHumanState : MoveHumanState
    {

        public MoveToCheckoutHumanState(
            Human human,
            HumanStateHandler stateMachine,
            Animator animator) : base(human, stateMachine, animator)
        {

        }

        public override void Enter()
        {
            base.Enter();
            human.agent.destination = StoreController.Instance.checkoutPoint.position;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            //TODO: Check if human has reached its destination
            if (!human.agent.pathPending && human.agent.remainingDistance < 0.75f)
                stateMachine.ChangeState(human.payAtCheckout);
        }

        public override string ToString()
        {
            return "MoveToCheckout";
        }

    }
}
