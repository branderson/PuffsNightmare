using Assets.Scripts.Interfaces;
using Assets.Scripts.Level;
using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Player
{
    public class GameOverOnExit : StateMachineBehaviour
    {
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // Inform the player that the game should end
            animator.GetComponent<PlayerController>().GameOver();
        }
    }
}
