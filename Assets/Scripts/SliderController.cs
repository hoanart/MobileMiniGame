using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = SpawnManager.GetSpawnedObjCount;
        if (slider.value >= slider.maxValue)
        {
            GameManager.instance.State = GameState.GAMEOVER;
            GameManager.instance.GameOver();
            SpawnManager.GetSpawnedObjCount = 0;
        }
    }
}
