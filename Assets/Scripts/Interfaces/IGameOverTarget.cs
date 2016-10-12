using UnityEngine.EventSystems;

namespace Assets.Interfaces
{
    public interface IGameOverTarget : IEventSystemHandler
    {
        void GameOver();
    }
}