using UnityEngine.EventSystems;

namespace Assets.Scripts.Interfaces
{
    public interface IGameOverTarget : IEventSystemHandler
    {
        void GameOver();
    }
}