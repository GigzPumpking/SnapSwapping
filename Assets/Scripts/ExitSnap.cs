using UnityEngine;

public class ExitSnap : StateMachineBehaviour
{
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.Instance.GetPlayer().Swap();
        GameManager.Instance.GetPlayer().particleSystem.Stop();
    }
}
