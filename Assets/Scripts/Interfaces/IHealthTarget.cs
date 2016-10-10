using UnityEngine.EventSystems;

namespace Assets.Scripts.Interfaces
{
    public interface IHealthTarget : IEventSystemHandler
    {
        /// <summary>
        /// Whether the IHealthTarget should receive damage at this time
        /// </summary>
        /// <returns>
        /// Whether to damage the IHealthTarget
        /// </returns>
        bool AcceptDamage();

        /// <summary>
        /// Message informing IHealthTarget how much damage they've taken
        /// </summary>
        /// <param name="amount">
        /// Amount of damage taken
        /// </param>
        void Damaged(int amount);

        /// <summary>
        /// Message informing IHealthTarget that they have died
        /// </summary>
        void Die();
    }
}