using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace HumanStateManagement
{
    public class Human : MonoBehaviour
    {
        //Nav fields... public because we read from this in state machines
        public NavMeshAgent agent;

        /*
         * Note: state machines use these fields
         */
        public string nextItem; //next item on the shopper's list
        public int basketSize; //Number of shopping items collected


        //The human's states:
        public HumanStateHandler stateMachine;
        public AtRestHumanState atRest;
        public MoveToItemHumanState moveToItem;
        public CollectItemHumanState collectItem;

        // Start is called before the first frame update
        void Start()
        {
            stateMachine = new HumanStateHandler();

            atRest = new AtRestHumanState(this, stateMachine);
            moveToItem = new MoveToItemHumanState(this, stateMachine);
            collectItem = new CollectItemHumanState(this, stateMachine);

            stateMachine.Initialize(atRest);

            agent = GetComponent<NavMeshAgent>();

            //Add small random delays for shoppers to get moving
            StartCoroutine(WakeUpAfter(Random.Range(0.75f, 2.5f)));

        }

        // Update is called once per frame
        void Update()
        {
            stateMachine.CurrentState.Update();
        }


        public void OnFinishPickup()
        {
            basketSize++;

            //TODO: either get another item or go to checkout
            nextItem = StoreController.Instance.store.GetRandomItem();
            agent.destination = StoreController.Instance.store.GetItemLocation(nextItem);
        }


        IEnumerator WakeUpAfter(float time)
        {
            yield return new WaitForSeconds(time);

            nextItem = StoreController.Instance.store.GetRandomItem();
            agent.destination = StoreController.Instance.store.GetItemLocation(nextItem);

        }
    }

}
