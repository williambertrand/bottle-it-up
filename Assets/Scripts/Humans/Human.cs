using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;


namespace HumanStateManagement
{
    public class Human : MonoBehaviour
    {

        public static float HUMAN_RUN_SPEED = 4.0f;
        public static float BASE_HUMAN_SPEEED = 0.75f;

        //Nav fields... public because we read from this in state machines
        public NavMeshAgent agent;

        /*
         * Note: state machines use these fields
         */
        public string nextItem; //next item on the shopper's list
        public int basketSize; //Number of shopping items collected
        public int listSize; //number of items on this shopper's list

        [SerializeField]
        public string[] shoppingList;

        //The human's shopping states
        public HumanStateHandler stateMachine;
        public AtRestHumanState atRest;
        public MoveToItemHumanState moveToItem;
        public CollectItemHumanState collectItem;
        public MoveToCheckoutHumanState moveToCheckout;
        public PayAtCheckoutHumanState payAtCheckout;
        public ExitStoreHumanState exitStore;

        public FearedHumanState fearedState;

        //NOTE: When a state does not rely on any particulars of the human
        //      for which it is being used, it should be defined as a static state.
        //      TODO: ExitStore should be able to be static if we set the human nav agent destination in the patAtCheckout state exit

        // Start is called before the first frame update
        void Start()
        {

            HumanSight sight = GetComponent<HumanSight>();

            stateMachine = new HumanStateHandler();

            atRest = new AtRestHumanState(this, stateMachine);
            moveToItem = new MoveToItemHumanState(this, stateMachine);
            collectItem = new CollectItemHumanState(this, stateMachine);
            moveToCheckout = new MoveToCheckoutHumanState(this, stateMachine);
            payAtCheckout = new PayAtCheckoutHumanState(this, stateMachine);
            exitStore = new ExitStoreHumanState(this, stateMachine);
            fearedState = new FearedHumanState(this, stateMachine, sight.player);

            stateMachine.Initialize(atRest);

            agent = GetComponent<NavMeshAgent>();
            agent.speed = BASE_HUMAN_SPEEED; //TODO: could randomize this

            listSize = Random.Range(3, 8);

            //Useful for testing: Add time delay for shoppers to get moving
            StartCoroutine(WakeUpAfter(0.25f));

        }

        // Update is called once per frame
        void Update()
        {
            if(stateMachine == null || stateMachine.CurrentState == null)
            {
                Debug.LogError("NO Current state");
            }
            stateMachine.CurrentState.Update();
        }

        /* Collect item, Get Next Item */
        public void OnFinishPickup()
        {
            basketSize++;
            if (shoppingList.Length != 0 && basketSize < shoppingList.Length)
            {
                nextItem = shoppingList[basketSize];
                agent.destination = StoreController.Instance.store.GetItemLocation(nextItem);
            }
            else if (basketSize < listSize)
            {
                nextItem = StoreController.Instance.store.GetRandomItem();
                agent.destination = StoreController.Instance.store.GetItemLocation(nextItem);
            }
            else
            {
                nextItem = null;
            }
        }


        IEnumerator WakeUpAfter(float time)
        {
            yield return new WaitForSeconds(time);

            nextItem = StoreController.Instance.store.GetRandomItem();
            agent.destination = StoreController.Instance.store.GetItemLocation(nextItem);

        }

        private void OnDrawGizmos()
        {
            if(stateMachine != null)
            {
                string currentStateStr = stateMachine.CurrentState.ToString() + " (" + agent.speed + ")";
                Handles.Label(transform.position + transform.up * 1.5f, currentStateStr);
            }
        }
    }

}
