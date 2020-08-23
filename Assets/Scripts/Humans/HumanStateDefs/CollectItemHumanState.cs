using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HumanStateManagement
{
    //TODO: Should probably define a MovingState that we inherit from for moving to item, to checkout, or to exit
    public class CollectItemHumanState : HumanState
    {
        public const float MAX_PICKUP = 3.0f;
        public const float MIN_PICKUP = 1.25f;

        private float timeToCollect;
        private float startTime;
        private string nextItem;

        public CollectItemHumanState(Human human, HumanStateHandler stateMachine) : base(human, stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            //item = human.NextItem
            //Set destination position based on next item on "shopping" list
            timeToCollect = Random.Range(MIN_PICKUP, MAX_PICKUP);
            startTime = Time.time;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Pause()
        {
            base.Pause();
            //Save item that was being collected?
            nextItem = human.nextItem;
        }

        public override void Resume()
        {
            base.Resume();
            //At this point we are away from the collect item, so probably need
            // to go bakc into moveToItemState
            human.agent.destination = StoreController.Instance.store.GetItemLocation(nextItem);
            stateMachine.ChangeState(human.moveToItem);
        }

        public override void Update()
        {
            base.Update();

            //TODO: Check if human has reached its destination

            if (Time.time > startTime + timeToCollect)
            {
                human.OnFinishPickup();
                if (human.basketSize == human.listSize)
                {
                    stateMachine.ChangeState(human.moveToCheckout);
                }
                else
                {
                    stateMachine.ChangeState(human.moveToItem);
                }
            }

        }

        public override string ToString()
        {
            return "Collecting";
        }
    }
}

