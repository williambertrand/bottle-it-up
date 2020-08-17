﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HumanStateManagement
{
    //TODO: Should probably define a MovingState that we inherit from for moving to item, to checkout, or to exit
    public class ExitStoreHumanState : HumanState
    {

        public ExitStoreHumanState(Human human, HumanStateHandler stateMachine) : base(human, stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            human.agent.destination = StoreController.Instance.exitPoint.position;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
        }

    }
}
