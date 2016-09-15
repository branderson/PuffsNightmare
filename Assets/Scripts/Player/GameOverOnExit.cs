using Assets.Scripts.Level;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class GameOverOnExit : StateMachineBehaviour
    {
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.GetComponent<PlayerUIController>().GameOver();
        }
    }
}
