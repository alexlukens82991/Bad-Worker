using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToBathroomState : StateMachineBehaviour
{
    NPC NPC;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (NPC == null)
            NPC = animator.GetComponentInParent<NPC>();

        animator.SetBool("IsIdle", false);

        NPC.FindToilet();
    }

}
