using Teams;

namespace Turns
{
    public interface ITurnTransitionable //лучше переделать в абстрактный класс с единой реализацией метода собскрайба
    {
        public void OnTurnTransition();
        public void SubscribeOnInit();
    }
}
