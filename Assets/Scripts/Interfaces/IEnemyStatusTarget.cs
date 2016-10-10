using UnityEngine.EventSystems;

namespace Assets.Scripts.Interfaces
{
    public interface IEnemyStatusTarget : IEventSystemHandler
    {
        void EnemySpawned();

        void EnemyDied(int score);
    }
}