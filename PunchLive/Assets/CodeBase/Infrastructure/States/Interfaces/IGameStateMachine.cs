using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.States.Interfaces
{
    public interface IGameStateMachine : IService
    {
        public void Enter<TState>() where TState : class, IState;
        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
    }
}