using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HumanStateManagement
{
    public class AtRestHumanState : HumanState
    {

        public AtRestHumanState(Human human, HumanStateHandler stateMachine) : base(human, stateMachine)
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

            //Check if human has another item to shop for, once it does, move on to moving state

            if (human.nextItem != null)
            {
                stateMachine.ChangeState(human.moveToItem);
            }

        }
    }
}
