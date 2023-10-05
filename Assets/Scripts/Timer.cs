using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    protected TMP_Text mText;

    [SerializeField]
    protected float mTime = 30.0f;

    [SerializeField]
    protected float mCurrentTime;

    protected int mSeconds;

    protected IEnumerator mStartTimerCoroutine;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        mStartTimerCoroutine = StartTimer();

    }

    private IEnumerator StartTimer()
    {
        mCurrentTime = mTime;

        while (mCurrentTime>0)
        {
            mCurrentTime -= Time.deltaTime;
            mSeconds = (int)mCurrentTime % 60;

            mText.text = mSeconds.ToString("00");
            yield return null;

            if (mCurrentTime <= 0)
            {
                Debug.Log("시간 종료");
                mCurrentTime = 0;
                SetGameState();
                yield break;
            }
        }
    }

    protected virtual void SetGameState()
    {
        
    }
}
