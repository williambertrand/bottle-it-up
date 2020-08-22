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

        //Use "pause" state when we plan to come right back to the old state
        //This is like a temporary Override of a state
        public void PushState(HumanState newState)
        {
            CurrentState.Pause();
            stateStack.Push(CurrentState);
            CurrentState = newState;
            newState.Enter();
        }

        public void PopState()
        {
            CurrentState.Exit();

            if (stateStack.Count == 0)
            {
                throw new Exception("Trying to pop from an empty state stack");
            }
            HumanState prevState = stateStack.Pop();
            CurrentState = prevState;
            prevState.Resume();
        }

    }
}