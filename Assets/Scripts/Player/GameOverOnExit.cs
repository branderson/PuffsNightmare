using UnityEngine;

namespace Assets.Player
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
