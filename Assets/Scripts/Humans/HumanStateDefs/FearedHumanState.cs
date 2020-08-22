using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace HumanStateManagement
{
    //TODO: Should probably define a MovingState that we inherit from for moving to item, to checkout, or to exit
    public class FearedHumanState : HumanState
    {

        public FearedHumanState(
            Human human,
            HumanStateHandler stateMachine,
            GameObject Player) : base(human, stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            //On Feaered enter: Pick a location away from the human and start running there

            float fearedRadius = 12.0f;


            Vector3 randomDirection = Random.insideUnitSphere * fearedRadius;
            randomDirection += human.transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, fearedRadius, 1);
            Vector3 finalPosition = hit.position;

            human.agent.speed = Human.HUMAN_RUN_SPEED;
            human.agent.destination = finalPosition;


            //TODO: Set feared/running animation here
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
            {
                Debug.Log("Human reached dest");
                //TODO: Check to see if player is still within FEAR distance,
                //if so, choose new location and keep running

                //else, just pop back to previous state
                stateMachine.PopState();
            }
        }

        public override string ToString()
        {
            return "FEARED";
        }

    }
}
