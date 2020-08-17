using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HumanStateManagement
{
    //TODO: define a WAITING type
    public class PayAtCheckoutHumanState : HumanState
    {
        private float timeToPay = 1.5f;
        private float startTime;

        public PayAtCheckoutHumanState(Human human, HumanStateHandler stateMachine) : base(human, stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            startTime = Time.time;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            //TODO: Check if human has reached its destination
            if (Time.time > startTime + timeToPay)
                stateMachine.ChangeState(human.exitStore);
        }
    }
}
