using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : StateMachineBehaviour // 進入State時播放音效
{
    // 要播放的音效名稱
    public string source;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SoundManager.PlaySound(source);
    }
}
