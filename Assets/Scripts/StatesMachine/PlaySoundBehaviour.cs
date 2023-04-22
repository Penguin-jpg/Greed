using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundBehaviour : StateMachineBehaviour
{
    public string source;
    public float volume = 1f;
    public bool playerOnEnter = true, playOnExit = false, playAfterDelay = false;
    public float playDelay = 0.25f;
    private float timeElapsed = 0f;
    private bool hasDelayedSoundPlayed = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(playerOnEnter)
        {
            SoundManager.PlaySound(source);
        }
        timeElapsed = 0f;
        hasDelayedSoundPlayed = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(playAfterDelay && !hasDelayedSoundPlayed)
        {
            timeElapsed += Time.deltaTime;
            if(timeElapsed > playDelay)
            {
                SoundManager.PlaySound(source);
                hasDelayedSoundPlayed = true;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
