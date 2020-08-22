using System.Collections.Generic;
using System;

namespace HumanStateManagement
{
    public class HumanStateHandler
    {
        public HumanState CurrentState { get; private set; }

        //Useful when we have a state we want to temporarily be in and then return to prev state
        public Stack<HumanState> stateStack;


        public void Initialize(HumanState startingState)
        {
            CurrentState = startingState;
            startingState.Enter();
            stateStack = new Stack<HumanState>();
        }

        public void ChangeState(HumanState newState)
        {
            CurrentState.Exit();

            CurrentState = newState;
            newState.Enter();
        }

        public void PushState(HumanState newState)
        {
            stateStack.Push(CurrentState);
            ChangeState(newState);
        }

        public void PopState()
        {
            if (stateStack.Count == 0)
            {
                throw new Exception("Trying to pop from an empty state stack");
            }
            HumanState prevState = stateStack.Pop();
            ChangeState(prevState);
        }
    }
}