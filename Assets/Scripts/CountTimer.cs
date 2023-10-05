using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountTimer : Timer
{
    private IEnumerator waitForStart;
    
    // Start is called before the first frame update
   protected override void Start()
    {
        base.Start();
        mTime = 30.0f;
        waitForStart = WaitCoroutine();
        StartCoroutine(waitForStart);

    }

   private IEnumerator WaitCoroutine()
   {
       while (GameManager.instance.State!=GameState.ONGOING)
       {
           yield return null;
       }
       StartCoroutine(mStartTimerCoroutine);
   }
   protected override void SetGameState()
   {
       base.SetGameState();
       GameManager.instance.State = GameState.GAMEOVER;
       GameManager.instance.GameOver();
   }
}
