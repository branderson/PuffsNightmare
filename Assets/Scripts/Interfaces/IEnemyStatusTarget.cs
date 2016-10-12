using UnityEngine.EventSystems;

namespace Assets.Interfaces
{
    public interface IEnemyStatusTarget : IEventSystemHandler
    {
        void EnemySpawned();

        void EnemyDied(int score);
    }
}