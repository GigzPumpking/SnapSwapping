using UnityEngine;

public class EnterSetState : StateMachineBehaviour
{
    [SerializeField] private bool setState;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("State", setState);
    }
}
