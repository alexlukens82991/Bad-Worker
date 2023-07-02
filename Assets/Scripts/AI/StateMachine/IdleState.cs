using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateMachineBehaviour
{
    private NPC thisNpc;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (thisNpc == null)
            thisNpc = animator.GetComponentInParent<NPC>();

        if (!animator.GetBool("HasTask"))
            thisNpc.GoToWorkStation();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (thisNpc == null)
            thisNpc = animator.GetComponentInParent<NPC>();

        thisNpc.SetRoamRoutineActive(false);
    }
}
