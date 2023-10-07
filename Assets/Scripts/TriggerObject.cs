using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    [SerializeField]
    private AnchorGameObject anchor;
    [SerializeField]
    private bool trans;

    [SerializeField]
    public TMP_Text scoreText;
    
    [SerializeField]
    public int score =100;
    private void OnEnable()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        anchor = GetComponent<AnchorGameObject>();
    }
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col.tag);
        if (GameManager.instance.State != GameState.ONGOING)
        {
            return;
        }
        if (col.CompareTag("Player"))
        {
            Debug.Log("충돌");
            IncreaseScore();
            SpawnManager.GetSpawnedObjCount -= 1;
            Debug.Log($"GetSpawned : {SpawnManager.GetSpawnedObjCount}");
            gameObject.SetActive(false);
        }
    }

    private void IncreaseScore()
    {
        int? currNum = int.Parse(scoreText.text);
        currNum += score;
        scoreText.text = currNum.ToString();
    }
}
