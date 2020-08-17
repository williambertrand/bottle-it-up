
namespace HumanStateManagement
{
    public class HumanStateHandler
    {
        public HumanState CurrentState { get; private set; }

        public void Initialize(HumanState startingState)
        {
            CurrentState = startingState;
            startingState.Enter();
        }

        public void ChangeState(HumanState newState)
        {
            CurrentState.Exit();

            CurrentState = newState;
            newState.Enter();
        }
    }
}