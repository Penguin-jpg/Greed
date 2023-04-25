using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatChange : StateMachineBehaviour
{
    // animator的float參數名稱
    public string parameter;
    // 由於某些情況下只有在離開時或進入時才要更新，所以就特別區分
    public bool updateOnStateEnter, updateOnStateMachineEnter, updateOnStateExit, updateOnStateMachineExit;
    public float valueOnEnter, valueOnExit;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnStateEnter)
        {
            animator.SetFloat(parameter, valueOnEnter);
        }
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnStateExit)
        {
            animator.SetFloat(parameter, valueOnExit);
        }
    }

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachineEnter)
        {
            animator.SetFloat(parameter, valueOnEnter);
        }
    }

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachineExit)
        {
            animator.SetFloat(parameter, valueOnExit);
        }
    }
}
