using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text mText;

    [SerializeField]
    private float mTime;

    [SerializeField]
    private float mCurrentTime;

    private int mSeconds;

    private IEnumerator mStartTimerCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        mTime = 30.0f;
        mStartTimerCoroutine = StartTimer();
        StartCoroutine(mStartTimerCoroutine);
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
                yield break;
            }
        }
        
    }
}
