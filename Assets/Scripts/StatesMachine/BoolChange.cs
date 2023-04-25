using System.Collections;
using UnityEngine;

namespace Assets.Scripts.StatesMachine
{
    public class BoolChange : StateMachineBehaviour
    {
        // animtor的bool參數名稱
        public string parameter;
        // 進入State時更新、進入StateMachine時更新
        public bool updateOnState, updateOnStateMachine;
        // 進入時候的值、離開時候的值
        public bool valueOnEnter, valueOnExit;

        // 進入State時做的事
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (updateOnState)
            {
                animator.SetBool(parameter, valueOnEnter);
            }
        }

        // 離開State時做的事
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (updateOnState)
            {
                animator.SetBool(parameter, valueOnExit);
            }
        }

        // 進入StateMachine時做的事
        override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            if (updateOnStateMachine)
            {
                animator.SetBool(parameter, valueOnEnter);
            }
        }

        // 離開StateMachine時做的事
        override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            if (updateOnStateMachine)
            {
                animator.SetBool(parameter, valueOnExit);
            }
        }
    }
}