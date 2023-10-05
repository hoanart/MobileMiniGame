using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToStartTimer : Timer
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        mTime = 3.0f;
        StartCoroutine(mStartTimerCoroutine);
    }

    protected override void SetGameState()
    {
        base.SetGameState();
        GameManager.instance.State = GameState.ONGOING;
        mText.enabled = false;
    }
    
}
