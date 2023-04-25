using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCorpse : StateMachineBehaviour
{
    // 消失時間
    public float fadeTime = 0.5f;
    // 消失延遲(幾秒之後才會開始消失)
    public float fadeDelay = 0.0f;
    private float timeElapsed = 0f;
    private float fadeDelayElapsed = 0f;
    // 該生物的SpriteRenderer
    SpriteRenderer spriteRenderer;
    // 該生物的Sprite顏色
    Color spriteColor;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        spriteColor = spriteRenderer.color;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 先延遲一段時間
        if (fadeDelay > fadeDelayElapsed)
        {
            fadeDelayElapsed += Time.deltaTime;
        }
        else
        {
            // 開始消失
            timeElapsed += Time.deltaTime;
            float newAlpha = spriteColor.a * (1 - timeElapsed / fadeTime);
            spriteRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, newAlpha);
            if (timeElapsed > fadeTime)
            {
                Destroy(animator.gameObject);
            }
        }
    }
}
