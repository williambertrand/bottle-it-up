using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace HumanStateManagement
{
    //TODO: Should probably define a MovingState that we inherit from for moving to item, to checkout, or to exit
    public class FearedHumanState : HumanState
    {
        GameObject player;
        Animator animator;

        public FearedHumanState(
            Human human,
            HumanStateHandler stateMachine,
            GameObject Player,
            Animator animator) : base(human, stateMachine)
        {
            player = Player;
            this.animator = animator;
        }

        public override void Enter()
        {
            base.Enter();
            //On Feaered enter: Pick a location away from the human and start running there

            float fearedRadius = 12.0f;

            Vector3 dirFromPlayer = (human.transform.position - player.transform.position).normalized;
            Vector3 runDirection = Quaternion.Euler(0, 0, Random.Range(-50f, 50f)) * dirFromPlayer;
            Vector3 runDest = runDirection * fearedRadius + human.transform.position;
            Debug.DrawLine(player.transform.position, human.transform.position + dirFromPlayer * fearedRadius, Color.blue, 2.0f);

            NavMeshHit hit;
            NavMesh.SamplePosition(runDest, out hit, fearedRadius, 1);
            Vector3 finalPosition = hit.position;

            human.agent.speed = Human.HUMAN_RUN_SPEED;
            human.agent.destination = finalPosition;

            animator.SetTrigger("Run");
        }

        public override void Exit()
        {
            base.Exit();
            human.agent.speed = Human.BASE_HUMAN_SPEEED;
        }

        public override void Update()
        {
            base.Update();

            //TODO: Check if human has reached its destination
            if (!human.agent.pathPending && human.agent.remainingDistance < 0.6f)
            {
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
