using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HumanStateManagement
{
    public class HumanSight : MonoBehaviour
    {

        public const float PLAYER_HEIGHT = 1.0f; //TODO this should be loaded from the player instance

        public float fieldOfView = 110f; //Field of view in degrees
        public bool playerInSight;
        public SphereCollider sightCollider;

        public PlayerController player;
        public Human human;

        // Start is called before the first frame update
        void Start()
        {
            sightCollider = GetComponent<SphereCollider>();
            human = GetComponent<Human>();
            player = PlayerController.Instance;

        }

        void Update()
        {

        }



        /***
         *  Conditions for player to be in sight:
         *  - Within trigger
         *  - within human FOV
         *  - No obstacles blocking
         *
         *  Note: we consider the radius of sightCollider to be equal to how far
         *  a human can see
         * 
         */
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject == player)
            {
                playerInSight = false;
                //If angle between human forward and player is half of FOV, in sight
                Vector3 dir = new Vector3(other.transform.position.x, PLAYER_HEIGHT, other.transform.position.z) - transform.position;
                float angle = Vector3.Angle(dir, transform.forward);

                if (angle <= 0.5f * fieldOfView)
                {
                    //Check for obstacle
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position + transform.up * PLAYER_HEIGHT,
                        dir.normalized,
                        out hit,
                        sightCollider.radius))
                    {
                        if (hit.collider.gameObject == player.gameObject)
                        {
                            playerInSight = true;
                            //If we need to handle keeping track of last player position, do it here
                            //lastSightingPos = player.transform.position or whatever

                            if (player.IsMonster)
                            {
                                if (human.stateMachine.CurrentState != human.fearedState)
                                {
                                    human.stateMachine.PushState(human.fearedState);
                                }

                            }

                            Debug.DrawLine(transform.position + transform.up * PLAYER_HEIGHT, transform.position + dir, Color.red, 1.0f);
                        }
                        
                    }
                }
            }
        }
    }
}
